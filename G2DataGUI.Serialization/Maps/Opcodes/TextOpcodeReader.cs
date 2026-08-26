using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Maps;

namespace G2DataGUI.Serialization.Maps.Opcodes;

/// <summary>
/// Not a normal opcode read: this consumes raw bytes as text until it hits
/// a real opcode boundary, other than NextLine (NextLine inside a text
/// run becomes an embedded '\n' and parsing continues), then backs the
/// stream up so the caller re-reads that byte as the next real opcode.
///
/// IMPORTANT — this ACTUALLY ATTEMPTS the dispatch for every byte
/// MapOpcodeReader.IsDispatchable says is top-level dispatchable, rather
/// than trusting that check alone and assuming success. TextBox/
/// Highlight/Pause/Unknown10 each read a SECOND byte (their own
/// sub-option) and can still fail — a byte IsDispatchable says has a
/// reader (e.g. Unknown10 = 0x10) can still produce null overall if that
/// second byte isn't a recognized TextBoxOption/HighlightOption/
/// PauseOption/Unknown10Option. IsDispatchable alone can't see that;
/// only actually calling ReadFromOpcode can. Confirmed against a real
/// map (1010.mdt) where exactly this happened — a bare 0x10 byte with a
/// following byte (0x09) that isn't a valid Unknown10Option looked
/// "already at a boundary" under the IsDispatchable-only check, so this
/// reader backed out having consumed nothing, and the caller looped back
/// into it with the identical byte forever. This version instead treats
/// "dispatchable but the actual attempt returned null" the same as "not
/// dispatchable at all": absorb the byte as text and keep going,
/// guaranteed at least 1 byte of progress every time through the loop.
///
/// A section legitimately ending mid-text (no trailing opcode byte to
/// confirm the boundary) is the ORDINARY case, not an error — this reader
/// always needs to peek one byte past the text to decide whether it's a
/// real opcode, and if the section simply has no more bytes at that
/// point, that peek hitting end-of-stream just means the text (and the
/// section) is over. Confirmed against real dialogue data: every single
/// section across every test file ends this way, so treating it as an
/// exceptional failure (as an earlier version of this fix did — letting
/// EndOfStreamException propagate all the way up) meant literally no
/// dialogue text was ever successfully extracted. Caught locally here and
/// treated as "return whatever text has accumulated so far," not
/// re-thrown.
/// </summary>
public sealed class TextOpcodeReader : IDataReader<TextOpcode>
{
	public TextOpcode Read(Stream stream)
	{
		TextOpcode opcode = new();

		while (true)
		{
			long position = stream.Position;
			byte data;

			try
			{
				data = stream.ReadRawByte();
			}
			catch (EndOfStreamException)
			{
				return opcode;
			}

			if (data == (byte)DialogueOpcode.NextLine)
			{
				opcode.Text += "\n";
				continue;
			}

			if (MapOpcodeReader.IsDispatchable(data))
			{
				IMapOpcode? attempted;

				try
				{
					attempted = MapOpcodeReader.ReadFromOpcode(stream, (DialogueOpcode)data);
				}
				catch (EndOfStreamException)
				{
					// The attempted opcode needed more bytes than the
					// section has left — same graceful stop as above; the
					// byte that looked like it might start an opcode
					// never actually turned into one.
					stream.Seek(position, SeekOrigin.Begin);
					return opcode;
				}

				if (attempted is not null)
				{
					// Genuine opcode boundary — stop the text run here and
					// let the caller re-dispatch this same byte properly.
					stream.Seek(position, SeekOrigin.Begin);
					return opcode;
				}

				// IsDispatchable said this byte's top-level opcode has a
				// reader, but the reader itself still failed (an
				// unrecognized sub-option byte). Treat the ORIGINAL byte
				// as text and continue from right after it, discarding
				// whatever the failed sub-dispatch attempted to read.
				stream.Seek(position + 1, SeekOrigin.Begin);
				opcode.Text += Convert.ToChar(data);
				continue;
			}

			opcode.Text += Convert.ToChar(data);
		}
	}

	public async Task<TextOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
