using G2DataGUI.Domain.Maps.Opcodes.Unknown0D;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Unknown0D;

public sealed class Unknown0DUnknown2OpcodeReader : IDataReader<Unknown0DUnknown2Opcode>
{
	public Unknown0DUnknown2Opcode Read(Stream stream) => new();

	public async Task<Unknown0DUnknown2Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
