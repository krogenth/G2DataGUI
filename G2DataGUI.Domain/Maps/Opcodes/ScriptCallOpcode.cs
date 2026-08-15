namespace G2DataGUI.Domain.Maps.Opcodes;

public sealed class ScriptCallOpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.ScriptCall;

	public byte ScriptId { get; set; }
	public byte CharacterId { get; set; }
}
