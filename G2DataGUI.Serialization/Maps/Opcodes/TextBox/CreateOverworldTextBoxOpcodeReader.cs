using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes.TextBox;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.TextBox;

public sealed class CreateOverworldTextBoxOpcodeReader : IDataReader<CreateOverworldTextBoxOpcode>
{
	public CreateOverworldTextBoxOpcode Read(Stream stream)
	{
		CreateOverworldTextBoxOpcode opcode = new()
		{
			TextBoxLength = stream.ReadRawByte(),
			TextBoxHeight = stream.ReadRawByte(),
			NestedOpcodes = TextBoxNestedOpcodeReader.ReadUntilTerminator(stream, extraTerminatorByte: null)
		};

		return opcode;
	}

	public async Task<CreateOverworldTextBoxOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
