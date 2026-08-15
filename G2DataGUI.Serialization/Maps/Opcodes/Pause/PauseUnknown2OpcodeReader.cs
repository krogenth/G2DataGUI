using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes.Pause;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Pause;

public sealed class PauseUnknown2OpcodeReader : IDataReader<PauseUnknown2Opcode>
{
	public PauseUnknown2Opcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte()
	};

	public async Task<PauseUnknown2Opcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
