using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Domain.Maps.Opcodes.Unknown16;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Maps.Opcodes.Unknown16;

/// <summary>
/// Sub-dispatches on a first byte (Unknown16Option) — same shape as
/// PauseOpcodeReader, just with 10 variants instead of 2. Replaces an
/// earlier version that read a single fixed 5-byte payload unconditionally;
/// that assumption happened to be exactly right for 4 of the 10 real
/// variants and silently wrong for the other 6 — see IUnknown16Opcode's
/// own doc comment for how this was actually confirmed (full-corpus
/// byte-count verification against ~600KB of real dialogue data, plus
/// two ground-truth examples provided directly).
/// </summary>
public sealed class Unknown16OpcodeReader : IDataReader<IMapOpcode?>
{
	public IMapOpcode? Read(Stream stream)
	{
		byte optionByte = stream.ReadRawByte();

		if (!Enum.IsDefined(typeof(Unknown16Option), optionByte))
		{
			return null;
		}

		return (Unknown16Option)optionByte switch
		{
			Unknown16Option.Unknown0 => new Unknown16Unknown0OpcodeReader().Read(stream),
			Unknown16Option.Unknown1 => new Unknown16Unknown1OpcodeReader().Read(stream),
			Unknown16Option.Unknown10 => new Unknown16Unknown10OpcodeReader().Read(stream),
			Unknown16Option.Unknown11 => new Unknown16Unknown11OpcodeReader().Read(stream),
			Unknown16Option.Unknown12 => new Unknown16Unknown12OpcodeReader().Read(stream),
			Unknown16Option.Unknown14 => new Unknown16Unknown14OpcodeReader().Read(stream),
			Unknown16Option.Unknown15 => new Unknown16Unknown15OpcodeReader().Read(stream),
			Unknown16Option.Unknown20 => new Unknown16Unknown20OpcodeReader().Read(stream),
			Unknown16Option.Unknown21 => new Unknown16Unknown21OpcodeReader().Read(stream),
			Unknown16Option.Unknown22 => new Unknown16Unknown22OpcodeReader().Read(stream),
			_ => null,
		};
	}

	public async Task<IMapOpcode?> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
