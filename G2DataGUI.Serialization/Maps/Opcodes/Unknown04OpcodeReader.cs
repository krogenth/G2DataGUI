using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

public sealed class Unknown04OpcodeReader : IDataReader<Unknown04Opcode>
{
	public Unknown04Opcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte()
	};

	public async Task<Unknown04Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
