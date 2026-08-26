using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes.TextBox;
using G2DataGUI.Serialization.Maps;
using G2DataGUI.Serialization.Maps.Opcodes;

namespace G2DataGUI.Serialization.Maps.Opcodes.TextBox;

/// <summary>
/// Shared loop for every ITextBoxOpcode variant except Remove: read a byte,
/// and either (a) it's a real, dispatchable opcode that ACTUALLY dispatches
/// successfully — add the result, and stop once that result is itself an
/// ITextBoxOpcode (terminator), or (b) it isn't (either not dispatchable
/// at all, or dispatchable at the top level but the sub-dispatch itself
/// failed) — back up and read it as text instead. CreateOptionsTextBoxOpcode
/// additionally treats a raw 0x02 byte as its own terminator
/// (extraTerminatorByte) — every other variant passes null.
///
/// The "dispatchable at the top level but the actual attempt still
/// returns null" case matters here specifically — TextBox/Highlight/
/// Pause/Unknown10 each read a second, sub-option byte that can be
/// unrecognized even when the FIRST byte has a wired reader. The
/// original version of this fix only checked MapOpcodeReader.
/// IsDispatchable(data) up front and assumed a true result meant success;
/// when the actual ReadFromOpcode call still returned null, this loop
/// fell through without seeking back or making progress — the exact
/// scenario that caused a confirmed infinite loop on real dialogue data
/// (a bare Unknown10 byte followed by an unrecognized Unknown10Option
/// byte). Now explicitly seeks back to the position BEFORE the failed
/// attempt and reads it as text instead, same as TextOpcodeReader.
///
/// Also treats running out of section before finding a real terminator
/// opcode as an implicit, valid stop rather than an error — same
/// reasoning as TextOpcodeReader's own doc comment on why that's the
/// ordinary case here, not a malformed-data signal. Whatever nested
/// opcodes/text decoded successfully before that point are kept.
/// </summary>
internal static class TextBoxNestedOpcodeReader
{
	public static List<IMapOpcode> ReadUntilTerminator(Stream stream, byte? extraTerminatorByte)
	{
		List<IMapOpcode> nested = [];

		while (true)
		{
			if (stream.Position >= stream.Length)
			{
				return nested;
			}

			long position = stream.Position;
			byte data;

			try
			{
				data = stream.ReadRawByte();
			}
			catch (EndOfStreamException)
			{
				return nested;
			}

			if (MapOpcodeReader.IsDispatchable(data))
			{
				IMapOpcode? nestedOpcode;

				try
				{
					nestedOpcode = MapOpcodeReader.ReadFromOpcode(stream, (DialogueOpcode)data);
				}
				catch (EndOfStreamException)
				{
					return nested;
				}

				if (nestedOpcode is not null)
				{
					nested.Add(nestedOpcode);

					if (nestedOpcode is ITextBoxOpcode)
					{
						return nested;
					}

					continue;
				}

				// Top-level dispatchable, but the sub-dispatch failed —
				// treat this byte as the start of a text run instead of
				// looping back with no progress made.
				stream.Seek(position, SeekOrigin.Begin);
				nested.Add(new TextOpcodeReader().Read(stream));
			}
			else if (extraTerminatorByte is not null && data == extraTerminatorByte)
			{
				return nested;
			}
			else
			{
				stream.Seek(position, SeekOrigin.Begin);
				nested.Add(new TextOpcodeReader().Read(stream));
			}
		}
	}
}
