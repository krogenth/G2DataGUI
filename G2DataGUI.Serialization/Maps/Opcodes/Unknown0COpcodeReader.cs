using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps.Opcodes;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes;

public sealed class Unknown0COpcodeReader : IDataReader<Unknown0COpcode>
{
	public Unknown0COpcode Read(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawByte()
	};

	public async Task<Unknown0COpcode> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
