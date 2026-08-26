namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>
/// Newly identified, not part of the original port — 0x0E is one of five
/// gaps in DialogueOpcode's own numeric range (no enum member at all
/// before this), which meant every occurrence was previously silently
/// skipped byte-by-byte as unrecognized filler. Confirmed as a real,
/// structured opcode against ~350KB of real dialogue data across 7 of
/// the game's largest maps: all 9 occurrences read as exactly 3 bytes
/// (`0x0E 00 00 00`), and every one immediately resyncs to a real opcode
/// or the known inter-block separator afterward. All 9 observed
/// occurrences have Unknown1/2/3 = 0 — purpose entirely unknown, and
/// whether these fields are ever non-zero is unconfirmed (the sample
/// just never showed it).
/// </summary>
public sealed class Unknown0EOpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown0E;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
}
