using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes.Pause;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Pause;

/// <summary>
/// Sub-dispatches on a second byte (PauseOption) — direct port of
/// the original IPauseOpcode.ReadOpcode. No recursion, unlike TextBox/Highlight.
/// </summary>
public sealed class PauseOpcodeReader : IDataReader<IMapOpcode?>
{
	public IMapOpcode? Read(Stream stream)
	{
		byte optionByte = stream.ReadRawByte();

		if (!Enum.IsDefined(typeof(PauseOption), optionByte))
		{
			return null;
		}

		return (PauseOption)optionByte switch
		{
			PauseOption.Unknown1 => new PauseUnknown1OpcodeReader().Read(stream),
			PauseOption.Unknown2 => new PauseUnknown2OpcodeReader().Read(stream),
			_ => null,
		};
	}

	public async Task<IMapOpcode?> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
