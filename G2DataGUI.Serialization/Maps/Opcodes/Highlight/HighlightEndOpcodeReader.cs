using G2DataGUI.Domain.Maps.Opcodes.Highlight;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Highlight;

public sealed class HighlightEndOpcodeReader : IDataReader<HighlightEndOpcode>
{
	public HighlightEndOpcode Read(Stream stream) => new();

	public async Task<HighlightEndOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
