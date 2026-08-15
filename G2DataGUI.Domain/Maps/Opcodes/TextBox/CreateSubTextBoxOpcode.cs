namespace G2DataGUI.Domain.Maps.Opcodes.TextBox;

/// <summary>A sub textbox that overlaps the primary textbox, top-right.</summary>
public sealed class CreateSubTextBoxOpcode : ITextBoxOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.TextBox;
	public TextBoxOption Option => TextBoxOption.CreateSubTextBox;
	public byte Unknown1 { get; set; }
	public byte TextBoxHeight { get; set; }
	public List<IMapOpcode> NestedOpcodes { get; set; } = [];
}
