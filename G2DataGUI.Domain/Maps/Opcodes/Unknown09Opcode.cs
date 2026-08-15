namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Completely unknown, may be 4 parameters.</summary>
public sealed class Unknown09Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown09;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public short StoryFlag { get; set; }
}
