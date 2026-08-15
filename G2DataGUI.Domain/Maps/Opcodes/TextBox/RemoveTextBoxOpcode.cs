namespace G2DataGUI.Domain.Maps.Opcodes.TextBox;

public sealed class RemoveTextBoxOpcode : ITextBoxOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.TextBox;
	public TextBoxOption Option => TextBoxOption.RemoveTextBox;
	public List<IMapOpcode> NestedOpcodes { get; } = [];
}
