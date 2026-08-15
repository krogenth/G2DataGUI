namespace G2DataGUI.Domain.Maps.Opcodes;

public sealed class CameraMoveOpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.CameraMove;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
}
