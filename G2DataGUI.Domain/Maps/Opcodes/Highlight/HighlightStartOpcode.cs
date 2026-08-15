namespace G2DataGUI.Domain.Maps.Opcodes.Highlight;

public sealed class HighlightStartOpcode : IHighlightOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Highlight;
	public HighlightOption Option => HighlightOption.Start;
	public List<IMapOpcode> NestedOpcodes { get; set; } = [];
}
