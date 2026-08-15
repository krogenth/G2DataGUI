using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes.TextBox;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.TextBox;

public sealed class CreateSubTextBoxOpcodeReader : IDataReader<CreateSubTextBoxOpcode>
{
	public CreateSubTextBoxOpcode Read(Stream stream)
	{
		CreateSubTextBoxOpcode opcode = new()
		{
			Unknown1 = stream.ReadRawByte(),
			TextBoxHeight = stream.ReadRawByte(),
			NestedOpcodes = TextBoxNestedOpcodeReader.ReadUntilTerminator(stream, extraTerminatorByte: null)
		};

		return opcode;
	}

	public async Task<CreateSubTextBoxOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
