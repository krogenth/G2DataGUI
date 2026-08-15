using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

public sealed class Unknown09OpcodeReader : IDataReader<Unknown09Opcode>
{
	public Unknown09Opcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		StoryFlag = stream.ReadRawShort()
	};

	public async Task<Unknown09Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
