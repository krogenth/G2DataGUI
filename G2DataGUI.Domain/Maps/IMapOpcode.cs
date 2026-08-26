namespace G2DataGUI.Domain.Maps;

/// <summary>
/// Marker for one opcode in a dialogue stream. A leading byte in the file
/// selects which concrete type to parse — see MapOpcodeReader for the
/// dispatch that replaces the old ParseDialogueOpcode switch statement.
/// This is exactly the case ICollectionDataReader/IDataWriter (batch, whole
/// file) doesn't fit: each opcode is a self-contained "read one T" op, so
/// each concrete opcode gets its own IDataReader<TOpcode>, and
/// MapOpcodeReader is itself an IDataReader<IMapOpcode> that picks
/// which one to delegate to based on the byte it just read.
/// </summary>
public interface IMapOpcode
{
	DialogueOpcode Opcode { get; }
}
