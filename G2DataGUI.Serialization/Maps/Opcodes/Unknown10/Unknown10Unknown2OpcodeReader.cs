using G2DataGUI.Domain.Maps.Opcodes.Unknown10;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Unknown10;

public sealed class Unknown10Unknown2OpcodeReader : IDataReader<Unknown10Unknown2Opcode>
{
	public Unknown10Unknown2Opcode Read(Stream stream) => new();

	public async Task<Unknown10Unknown2Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
