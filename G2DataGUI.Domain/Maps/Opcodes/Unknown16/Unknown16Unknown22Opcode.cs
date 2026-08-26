namespace G2DataGUI.Domain.Maps.Opcodes.Unknown16;

/// <summary>Variant 0x22. 4 parameter bytes after the type byte.</summary>
public sealed class Unknown16Unknown22Opcode : IUnknown16Opcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown16;
	public Unknown16Option Option => Unknown16Option.Unknown22;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
}
