namespace G2DataGUI.Domain.Maps.Opcodes.Unknown16;

/// <summary>Variant 0x20 — this is the exact variant a user's own examples confirmed (`16 20 FF 00 FF 00` and `16 20 02 00 00 00`, both 6 bytes total). 4 parameter bytes after the type byte.</summary>
public sealed class Unknown16Unknown20Opcode : IUnknown16Opcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown16;
	public Unknown16Option Option => Unknown16Option.Unknown20;

	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
}
