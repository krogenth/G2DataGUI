using G2DataGUI.Domain.Maps.Opcodes.TextBox;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.TextBox;

public sealed class RemoveTextBoxOpcodeReader : IDataReader<RemoveTextBoxOpcode>
{
	public RemoveTextBoxOpcode Read(Stream stream) => new();

	public async Task<RemoveTextBoxOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
