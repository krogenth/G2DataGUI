namespace G2DataGUI.Domain.Maps.Opcodes.Pause;

public sealed class PauseUnknown2Opcode : IPauseOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Pause;
	public PauseOption Option => PauseOption.Unknown2;
	public byte Unknown1 { get; set; }
}
