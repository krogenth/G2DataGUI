using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes.Unknown16;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Unknown16;

public sealed class Unknown16Unknown0OpcodeReader : IDataReader<Unknown16Unknown0Opcode>
{
	public Unknown16Unknown0Opcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte()
	};

	public async Task<Unknown16Unknown0Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
