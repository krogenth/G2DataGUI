using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

public sealed class CameraMoveOpcodeReader : IDataReader<CameraMoveOpcode>
{
	public CameraMoveOpcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte()
	};

	public async Task<CameraMoveOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
