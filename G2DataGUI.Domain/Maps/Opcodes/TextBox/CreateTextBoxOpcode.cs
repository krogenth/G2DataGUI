namespace G2DataGUI.Domain.Maps.Opcodes.TextBox;

public sealed class CreateTextBoxOpcode : ITextBoxOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.TextBox;
	public TextBoxOption Option => TextBoxOption.CreateTextBox;
	public List<IMapOpcode> NestedOpcodes { get; set; } = [];
}
