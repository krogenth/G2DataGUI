namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>
/// Completely unsure, likely 3 parameters — ported verbatim from
/// the original's own doc comment; field meaning is unknown.
/// </summary>
public sealed class Unknown03Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown03;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
}
