namespace G2DataGUI.Domain.Maps.Opcodes.Highlight;

public sealed class HighlightEndOpcode : IHighlightOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Highlight;
	public HighlightOption Option => HighlightOption.End;
	public List<IMapOpcode> NestedOpcodes { get; } = [];
}
