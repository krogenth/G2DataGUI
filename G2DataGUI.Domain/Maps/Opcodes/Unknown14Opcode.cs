namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Completely unknown, no clue on parameters — original reader
/// consumed 5 bytes, so ported as 5.</summary>
public sealed class Unknown14Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown14;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public byte Unknown5 { get; set; }
}
