using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

public sealed class Unknown12OpcodeReader : IDataReader<Unknown12Opcode>
{
	public Unknown12Opcode Read(Stream stream) => new();

	public async Task<Unknown12Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
