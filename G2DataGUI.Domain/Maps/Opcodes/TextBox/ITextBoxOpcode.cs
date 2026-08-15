namespace G2DataGUI.Domain.Maps.Opcodes.TextBox;

/// <summary>
/// A TextBox opcode carries a second dispatch byte (TextBoxOption) beyond
/// the leading DialogueOpcode.TextBox byte, and every variant except Remove
/// recursively parses further opcodes into NestedOpcodes until it hits
/// another ITextBoxOpcode (the terminator) — see TextBoxOpcodeReader.
/// </summary>
public interface ITextBoxOpcode : IMapOpcode
{
	TextBoxOption Option { get; }

	List<IMapOpcode> NestedOpcodes { get; }
}
