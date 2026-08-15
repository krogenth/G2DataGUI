using G2DataGUI.Domain.Maps.Opcodes.TextBox;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.TextBox;

public sealed class CreateTextBoxOpcodeReader : IDataReader<CreateTextBoxOpcode>
{
	public CreateTextBoxOpcode Read(Stream stream) => new()
	{
		NestedOpcodes = TextBoxNestedOpcodeReader.ReadUntilTerminator(stream, extraTerminatorByte: null)
	};

	public async Task<CreateTextBoxOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
