namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>
/// Completely unknown, likely 4 parameters.
/// </summary>
public sealed class Unknown15Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown15;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
}
