using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Domain.Maps.Opcodes.Highlight;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Maps;
using G2DataGUI.Serialization.Maps.Opcodes;

namespace G2DataGUI.Serialization.Maps.Opcodes.Highlight;

/// <summary>
/// Reads nested opcodes (recursing back through MapOpcodeReader, same as
/// TextBoxOpcodeReader) until it produces a HighlightEndOpcode specifically
/// — not just any IHighlightOpcode, matching the original's
/// `nestedOpcode is HighlightEndOpcode` check exactly (a nested
/// HighlightStartOpcode does NOT terminate its own parent).
///
/// Same "top-level dispatchable but the actual attempt still returns
/// null" handling as TextOpcodeReader/TextBoxNestedOpcodeReader — a byte
/// like Unknown10 (0x10) can have a wired reader yet still fail if its
/// own sub-option byte isn't recognized. Explicitly seeks back to the
/// position before the failed attempt and reads it as text, guaranteed
/// at least 1 byte of progress every iteration.
///
/// Also treats running out of section before finding a HighlightEndOpcode
/// as an implicit, valid stop rather than an error — same reasoning as
/// TextOpcodeReader's own doc comment.
/// </summary>
public sealed class HighlightStartOpcodeReader : IDataReader<HighlightStartOpcode>
{
	public HighlightStartOpcode Read(Stream stream)
	{
		HighlightStartOpcode opcode = new();

		while (true)
		{
			if (stream.Position >= stream.Length)
			{
				return opcode;
			}

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

			if (MapOpcodeReader.IsDispatchable(data))
			{
				IMapOpcode? nestedOpcode;

				try
				{
					nestedOpcode = MapOpcodeReader.ReadFromOpcode(stream, (DialogueOpcode)data);
				}
				catch (EndOfStreamException)
				{
					return opcode;
				}

				if (nestedOpcode is not null)
				{
					opcode.NestedOpcodes.Add(nestedOpcode);

					if (nestedOpcode is HighlightEndOpcode)
					{
						return opcode;
					}

					continue;
				}

				stream.Seek(position, SeekOrigin.Begin);
				opcode.NestedOpcodes.Add(new TextOpcodeReader().Read(stream));
			}
			else
			{
				stream.Seek(position, SeekOrigin.Begin);
				opcode.NestedOpcodes.Add(new TextOpcodeReader().Read(stream));
			}
		}
	}

	public async Task<HighlightStartOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
