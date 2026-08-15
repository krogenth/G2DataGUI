using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

public sealed class Unknown1BOpcodeReader : IDataReader<Unknown1BOpcode>
{
	public Unknown1BOpcode Read(Stream stream) => new();

	public async Task<Unknown1BOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
