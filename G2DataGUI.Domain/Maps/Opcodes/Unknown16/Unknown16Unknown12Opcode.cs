namespace G2DataGUI.Domain.Maps.Opcodes.Unknown16;

/// <summary>Variant 0x12 — this is the exact variant a user's own example confirmed (`16 12 00` immediately followed by a known `17`). 1 parameter byte after the type byte.</summary>
public sealed class Unknown16Unknown12Opcode : IUnknown16Opcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown16;
	public Unknown16Option Option => Unknown16Option.Unknown12;

	public byte Unknown1 { get; set; }
}
