using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

public sealed class NextLineOpcodeReader : IDataReader<NextLineOpcode>
{
	public NextLineOpcode Read(Stream stream) => new();

	public async Task<NextLineOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
