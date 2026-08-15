namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Completely unknown, may be 4 parameters — but the original
/// reader only ever consumed 3 bytes, so ported as 3.</summary>
public sealed class Unknown06Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown06;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
}
