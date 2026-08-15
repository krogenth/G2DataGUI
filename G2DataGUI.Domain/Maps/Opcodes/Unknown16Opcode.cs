namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Completely unknown, exists with the tutorial guy's dialogue.</summary>
public sealed class Unknown16Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown16;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public byte Unknown5 { get; set; }
}
