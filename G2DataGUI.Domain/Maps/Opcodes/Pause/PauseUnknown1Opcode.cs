namespace G2DataGUI.Domain.Maps.Opcodes.Pause;

public sealed class PauseUnknown1Opcode : IPauseOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Pause;
	public PauseOption Option => PauseOption.Unknown1;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
}
