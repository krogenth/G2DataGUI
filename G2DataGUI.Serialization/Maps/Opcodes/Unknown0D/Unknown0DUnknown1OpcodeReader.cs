using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes.Unknown0D;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Unknown0D;

public sealed class Unknown0DUnknown1OpcodeReader : IDataReader<Unknown0DUnknown1Opcode>
{
	public Unknown0DUnknown1Opcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawUShort(),
		Unknown2 = stream.ReadRawUShort(),
		Unknown3 = stream.ReadRawUShort(),
		Unknown4 = stream.ReadRawUShort()
	};

	public async Task<Unknown0DUnknown1Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
