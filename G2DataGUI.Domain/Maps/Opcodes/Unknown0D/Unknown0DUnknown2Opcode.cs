namespace G2DataGUI.Domain.Maps.Opcodes.Unknown0D;

/// <summary>The "short" form (type byte 0x01) — no further parameter
/// bytes; whatever follows in the stream (a real opcode, or a text run)
/// starts immediately after this type byte.</summary>
public sealed class Unknown0DUnknown2Opcode : IUnknown0DOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown0D;
	public Unknown0DOption Option => Unknown0DOption.Unknown2;
}
