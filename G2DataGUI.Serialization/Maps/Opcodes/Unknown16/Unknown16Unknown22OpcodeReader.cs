using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes.Unknown16;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Unknown16;

public sealed class Unknown16Unknown22OpcodeReader : IDataReader<Unknown16Unknown22Opcode>
{
	public Unknown16Unknown22Opcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		Unknown3 = stream.ReadRawByte(),
		Unknown4 = stream.ReadRawByte()
	};

	public async Task<Unknown16Unknown22Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
