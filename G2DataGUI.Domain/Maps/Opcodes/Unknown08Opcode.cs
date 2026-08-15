namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Completely unknown, likely 2 parameters — original reader
/// consumed 3 bytes, so ported as 3.</summary>
public sealed class Unknown08Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown08;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
}
