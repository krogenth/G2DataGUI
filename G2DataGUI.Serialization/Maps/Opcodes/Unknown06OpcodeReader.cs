using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

public sealed class Unknown06OpcodeReader : IDataReader<Unknown06Opcode>
{
	public Unknown06Opcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		Unknown3 = stream.ReadRawByte()
	};

	public async Task<Unknown06Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
