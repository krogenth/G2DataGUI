using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes.Unknown10;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Unknown10;

public sealed class Unknown10Unknown1OpcodeReader : IDataReader<Unknown10Unknown1Opcode>
{
	public Unknown10Unknown1Opcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		MapID = stream.ReadRawShort(),
		Unknown3 = stream.ReadRawByte()
	};

	public async Task<Unknown10Unknown1Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
