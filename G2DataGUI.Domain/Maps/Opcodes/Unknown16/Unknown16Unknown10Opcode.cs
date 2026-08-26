namespace G2DataGUI.Domain.Maps.Opcodes.Unknown16;

/// <summary>Variant 0x10. 8 parameter bytes after the type byte.</summary>
public sealed class Unknown16Unknown10Opcode : IUnknown16Opcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown16;
	public Unknown16Option Option => Unknown16Option.Unknown10;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public byte Unknown5 { get; set; }
	public byte Unknown6 { get; set; }
	public byte Unknown7 { get; set; }
	public byte Unknown8 { get; set; }
}
