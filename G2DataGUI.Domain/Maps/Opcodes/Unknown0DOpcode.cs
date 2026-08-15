namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Completely unknown, seems to have 2 parameters.</summary>
public sealed class Unknown0DOpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown0D;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
}
