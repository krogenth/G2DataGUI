using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

/// <summary>
/// Not a normal opcode read: this consumes raw bytes as text until it hits
/// one that IS a defined DialogueOpcode other than NextLine (NextLine
/// inside a text run becomes an embedded '\n' and parsing continues), then
/// backs the stream up one byte so the caller re-reads that byte as the
/// next real opcode. Ported behavior verbatim; the original wrapped this in
/// a try/catch that logged to a static Errors singleton — dropped along
/// with that singleton (see PORTING.md), so a malformed stream now throws
/// instead of silently truncating the text.
/// </summary>
public sealed class TextOpcodeReader : IDataReader<TextOpcode>
{
	public TextOpcode Read(Stream stream)
	{
		TextOpcode opcode = new();

		byte data = stream.ReadRawByte();

		while (!Enum.IsDefined(typeof(DialogueOpcode), data) || (DialogueOpcode)data == DialogueOpcode.NextLine)
		{
			opcode.Text += data == (byte)DialogueOpcode.NextLine ? "\n" : Convert.ToChar(data);
			data = stream.ReadRawByte();
		}

		stream.Seek(-1, SeekOrigin.Current);

		return opcode;
	}

	public async Task<TextOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
