using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

public sealed class ScriptCallOpcodeReader : IDataReader<ScriptCallOpcode>
{
	public ScriptCallOpcode Read(Stream stream) => new()
	{
		ScriptId = stream.ReadRawByte(),
		CharacterId = stream.ReadRawByte()
	};

	public async Task<ScriptCallOpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
