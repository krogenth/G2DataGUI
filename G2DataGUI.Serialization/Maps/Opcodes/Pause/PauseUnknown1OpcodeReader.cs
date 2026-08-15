using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes.Pause;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Pause;

public sealed class PauseUnknown1OpcodeReader : IDataReader<PauseUnknown1Opcode>
{
	public PauseUnknown1Opcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte()
	};

	public async Task<PauseUnknown1Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
