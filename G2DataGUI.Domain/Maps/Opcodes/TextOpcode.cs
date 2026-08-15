namespace G2DataGUI.Domain.Maps.Opcodes;

public sealed class TextOpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Text;

	public string Text { get; set; } = string.Empty;
}
