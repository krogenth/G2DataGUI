namespace G2DataGUI.Domain.Maps.Opcodes.Unknown16;

/// <summary>
/// Sub-dispatches on a first byte (Unknown16Option) — same shape as
/// IPauseOpcode/PauseOption, just with far more variants (10, vs Pause's
/// 2). Previously defined as a single fixed 5-byte opcode, which was
/// wrong: that assumption happened to be exactly right for 4 of the 10
/// real variants (0x14/0x20/0x21/0x22, all 4 parameter bytes after the
/// type byte) and silently wrong for the other 6, corrupting alignment
/// for everything that followed until the stream happened to resync by
/// chance. Confirmed via byte-count verification against ~600KB of real
/// dialogue data, cross-checked against two ground-truth examples a user
/// provided directly (0x12 and 0x20's exact byte sequences) rather than
/// purely from pattern-matching.
/// </summary>
public interface IUnknown16Opcode : IMapOpcode
{
	Unknown16Option Option { get; }
}
