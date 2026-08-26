using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes.Unknown0D;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Unknown0D;

/// <summary>
/// Sub-dispatches on a first byte (Unknown0DOption) — same shape as
/// PauseOpcodeReader. Replaces an earlier version that just read 2 fixed
/// bytes unconditionally and always "succeeded" regardless of their
/// actual value; that meant it could never signal failure back to a
/// caller deciding "is this really an opcode, or is it text" (unlike
/// TextBox/Highlight/Pause/Unknown10, which all read a validated
/// sub-option byte and can fail). Confirmed against real data that this
/// unconditional-success behavior was actively wrong: the real structure
/// is variable-length based on this first byte, and only 2 values
/// (0x00, 0x01) have ever been observed.
/// </summary>
public sealed class Unknown0DOpcodeReader : IDataReader<IMapOpcode?>
{
	public IMapOpcode? Read(Stream stream)
	{
		byte optionByte = stream.ReadRawByte();

		if (!Enum.IsDefined(typeof(Unknown0DOption), optionByte))
		{
			return null;
		}

		return (Unknown0DOption)optionByte switch
		{
			Unknown0DOption.Unknown1 => new Unknown0DUnknown1OpcodeReader().Read(stream),
			Unknown0DOption.Unknown2 => new Unknown0DUnknown2OpcodeReader().Read(stream),
			_ => null,
		};
	}

	public async Task<IMapOpcode?> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
