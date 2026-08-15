namespace G2DataGUI.Domain.Maps.Opcodes.TextBox;

/// <summary>A textbox at top-center, generally used for screen transition
/// labels (e.g. "to Carbo Village").</summary>
public sealed class CreateOverworldTextBoxOpcode : ITextBoxOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.TextBox;
	public TextBoxOption Option => TextBoxOption.CreateOverworldTextBox;
	public byte TextBoxLength { get; set; }
	public byte TextBoxHeight { get; set; }
	public List<IMapOpcode> NestedOpcodes { get; set; } = [];
}
