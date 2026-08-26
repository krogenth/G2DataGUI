namespace G2DataGUI.Domain.Maps;

/// <summary>
/// Raw byte values are load-bearing — they're the on-disk opcode
/// id, not just a display enum. Ported verbatim from the original,
/// including the "Completely unknown" gaps; see MapOpcodeReader for what's
/// actually implemented vs. still a documented TODO.
///
/// Unknown0E/Unknown0F/Unknown12 were added after the original port —
/// see their own opcode types' doc comments for the evidence (byte-level
/// analysis against ~350KB of real dialogue data across 7 of the game's
/// largest maps). Unknown13 and Unknown19 remain genuine gaps: 0 confirmed
/// occurrences across that same sample, so there's no evidence either way
/// on whether they're real, unused opcodes or not opcodes at all.
/// </summary>
public enum DialogueOpcode : byte
{
	Text = 0x00,
	Unknown03 = 0x03,
	Unknown04 = 0x04,
	ScriptCall = 0x05,
	Unknown06 = 0x06,
	Unknown07 = 0x07,
	Unknown08 = 0x08,
	Unknown09 = 0x09,
	Unknown0A = 0x0A,
	CameraMove = 0x0B,
	Unknown0C = 0x0C,
	Unknown0D = 0x0D,
	Unknown0E = 0x0E,
	Unknown0F = 0x0F,
	Unknown10 = 0x10,
	ItemAcquire = 0x11,
	Unknown12 = 0x12,
	Unknown14 = 0x14,
	Unknown15 = 0x15,
	Unknown16 = 0x16,
	TextBox = 0x17,
	CharacterPortrait = 0x18,
	NextPage = 0x1A,
	Unknown1B = 0x1B,
	Unknown1C = 0x1C,
	Pause = 0x1D,
	Highlight = 0x1E,
	NextLine = 0x1F,
}
