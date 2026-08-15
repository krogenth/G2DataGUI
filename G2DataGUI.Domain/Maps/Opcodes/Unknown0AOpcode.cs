namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>
/// Seems to be associated with the TextBox Options opcode, links
/// options to function handling.
/// </summary>
public sealed class Unknown0AOpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown0A;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public byte Unknown5 { get; set; }
	public byte Unknown6 { get; set; }
}
