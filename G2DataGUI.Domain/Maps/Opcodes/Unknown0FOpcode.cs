namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>
/// Newly identified, not part of the original port — 0x0F is one of five
/// gaps in DialogueOpcode's own numeric range. Confirmed as a real,
/// structured opcode against ~350KB of real dialogue data across 7 of
/// the game's largest maps: all 30 occurrences read as exactly 1 byte,
/// and every one resyncs to a real opcode, the known inter-block
/// separator, or another 0x0F opcode immediately following — they
/// frequently appear several in a row, e.g. `0f 43 0f 44 0f 46...`,
/// consistent with enumerating a short list of something. An index into
/// which is plausible but unconfirmed given the observed value range
/// (0x02 to 0xB3), hence Unknown1 rather than a guessed field name.
/// </summary>
public sealed class Unknown0FOpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown0F;

	public byte Unknown1 { get; set; }
}
