namespace G2DataGUI.Domain.Maps.Opcodes.Unknown16;

/// <summary>
/// Confirmed against a very large real-data sample (50+ dialogue files,
/// ~600KB total) via full-corpus byte-count verification — see
/// Unknown16Opcode's own doc comment for the numbered options'
/// parameter-byte counts and how each was established. Naming follows
/// the project's existing convention for "confirmed structure, unknown
/// meaning" (matching PauseOption/Unknown10Option) — numbered in the
/// order the raw byte values appear, not by any semantic grouping.
/// </summary>
public enum Unknown16Option : byte
{
	Unknown0 = 0x00,
	Unknown1 = 0x01,
	Unknown10 = 0x10,
	Unknown11 = 0x11,
	Unknown12 = 0x12,
	Unknown14 = 0x14,
	Unknown15 = 0x15,
	Unknown20 = 0x20,
	Unknown21 = 0x21,
	Unknown22 = 0x22,
}
