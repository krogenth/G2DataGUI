namespace G2DataGUI.Domain.Maps.Opcodes.TextBox;

/// <summary>A textbox in the top-left showing a list of options to choose from.</summary>
public sealed class CreateOptionsTextBoxOpcode : ITextBoxOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.TextBox;
	public TextBoxOption Option => TextBoxOption.CreateOptionsTextBox;
	public byte TextBoxLength { get; set; }
	public byte TextBoxHeight { get; set; }
	public List<IMapOpcode> NestedOpcodes { get; set; } = [];
}
