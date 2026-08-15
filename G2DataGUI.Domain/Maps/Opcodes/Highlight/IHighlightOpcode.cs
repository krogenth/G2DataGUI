namespace G2DataGUI.Domain.Maps.Opcodes.Highlight;

/// <summary>Creates a highlighted section of text. Start recursively parses
/// nested opcodes until it hits a HighlightEndOpcode — see HighlightOpcodeReader.</summary>
public interface IHighlightOpcode : IMapOpcode
{
	HighlightOption Option { get; }

	List<IMapOpcode> NestedOpcodes { get; }
}
