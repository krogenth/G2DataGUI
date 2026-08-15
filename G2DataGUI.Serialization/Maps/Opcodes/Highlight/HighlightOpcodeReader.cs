using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes.Highlight;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Highlight;

/// <summary>
/// Sub-dispatches on a second byte (HighlightOption) — direct port
/// of the original IHighlightOpcode.ReadOpcode.
/// </summary>
public sealed class HighlightOpcodeReader : IDataReader<IMapOpcode?>
{
	public IMapOpcode? Read(Stream stream)
	{
		byte optionByte = stream.ReadRawByte();

		if (!Enum.IsDefined(typeof(HighlightOption), optionByte))
		{
			stream.Seek(-1, SeekOrigin.Current);
			return null;
		}

		return (HighlightOption)optionByte switch
		{
			HighlightOption.Start => new HighlightStartOpcodeReader().Read(stream),
			HighlightOption.End => new HighlightEndOpcodeReader().Read(stream),
			_ => null,
		};
	}

	public async Task<IMapOpcode?> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
