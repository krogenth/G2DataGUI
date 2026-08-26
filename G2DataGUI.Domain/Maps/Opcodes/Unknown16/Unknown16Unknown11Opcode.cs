namespace G2DataGUI.Domain.Maps.Opcodes.Unknown16;

/// <summary>Variant 0x11. 1 parameter byte after the type byte.</summary>
public sealed class Unknown16Unknown11Opcode : IUnknown16Opcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown16;
	public Unknown16Option Option => Unknown16Option.Unknown11;

	public byte Unknown1 { get; set; }
}
