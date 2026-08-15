using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes.Unknown10;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Unknown10;

/// <summary>
/// Sub-dispatches on a second byte (Unknown10Option) — direct port
/// of the original IUnknown10Opcode.ReadOpcode.
/// </summary>
public sealed class Unknown10OpcodeReader : IDataReader<IMapOpcode?>
{
	public IMapOpcode? Read(Stream stream)
	{
		byte optionByte = stream.ReadRawByte();

		if (!Enum.IsDefined(typeof(Unknown10Option), optionByte))
		{
			stream.Seek(-1, SeekOrigin.Current);

			return null;
		}

		return (Unknown10Option)optionByte switch
		{
			Unknown10Option.Unknown1 => new Unknown10Unknown1OpcodeReader().Read(stream),
			Unknown10Option.Unknown2 => new Unknown10Unknown2OpcodeReader().Read(stream),
			_ => null,
		};
	}

	public async Task<IMapOpcode?> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
