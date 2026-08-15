using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Domain.Maps.Opcodes.Highlight;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Maps.Opcodes;

namespace G2DataGUI.Serialization.Maps.Opcodes.Highlight;

/// <summary>
/// Reads nested opcodes (recursing back through MapOpcodeReader, same as
/// TextBoxOpcodeReader) until it produces a HighlightEndOpcode specifically
/// — not just any IHighlightOpcode, matching the original's
/// `nestedOpcode is HighlightEndOpcode` check exactly (a nested
/// HighlightStartOpcode does NOT terminate its own parent).
/// </summary>
public sealed class HighlightStartOpcodeReader : IDataReader<HighlightStartOpcode>
{
	public HighlightStartOpcode Read(Stream stream)
	{
		HighlightStartOpcode opcode = new();

		while (true)
		{
			byte data = stream.ReadRawByte();

			if (Enum.IsDefined(typeof(DialogueOpcode), data))
			{
				IMapOpcode? nestedOpcode = MapOpcodeReader.ReadFromOpcode(stream, (DialogueOpcode)data);

				if (nestedOpcode is not null)
				{
					opcode.NestedOpcodes.Add(nestedOpcode);
				}

				if (nestedOpcode is HighlightEndOpcode)
				{
					return opcode;
				}
			}
			else
			{
				stream.Seek(-1, SeekOrigin.Current);
				opcode.NestedOpcodes.Add(new TextOpcodeReader().Read(stream));
			}
		}
	}

	public async Task<HighlightStartOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
