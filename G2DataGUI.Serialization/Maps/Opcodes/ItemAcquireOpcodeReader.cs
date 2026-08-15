using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

public sealed class ItemAcquireOpcodeReader : IDataReader<ItemAcquireOpcode>
{
	public ItemAcquireOpcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte(),
		Item1Offset = stream.ReadRawByte()
	};

	public async Task<ItemAcquireOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
