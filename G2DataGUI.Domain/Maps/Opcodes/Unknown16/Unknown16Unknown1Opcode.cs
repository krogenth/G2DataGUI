namespace G2DataGUI.Domain.Maps.Opcodes.Unknown16;

/// <summary>Variant 0x01. 2 parameter bytes after the type byte.</summary>
public sealed class Unknown16Unknown1Opcode : IUnknown16Opcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown16;
	public Unknown16Option Option => Unknown16Option.Unknown1;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
}
