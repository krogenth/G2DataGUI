namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>
/// Completely unknown purpose. Byte count was long a documented
/// uncertainty here — the original reader only ever consumed 1 byte,
/// with this type's own comment already noting "may be 1 or 4
/// parameters." Confirmed the 4-more-parameters case: verified against
/// ~350KB of real dialogue data across 7 of the game's largest maps,
/// every occurrence (11/11) only resyncs to a real opcode or the known
/// inter-block separator when read as 5 parameter bytes, not 1 — e.g.
/// `07 04 00 00 00 00 08...` (5 params: 04 00 00 00 00), immediately
/// followed by a valid Unknown08. Reading only 1 byte left the other 4
/// silently misread as the start of the next opcode, which is what was
/// producing long, unexplained runs of skipped bytes right after this
/// opcode.
/// </summary>
public sealed class Unknown07Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown07;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public byte Unknown5 { get; set; }
}
