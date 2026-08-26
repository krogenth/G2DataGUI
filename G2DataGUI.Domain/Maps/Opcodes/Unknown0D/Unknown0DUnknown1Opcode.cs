namespace G2DataGUI.Domain.Maps.Opcodes.Unknown0D;

/// <summary>The "long" form (type byte 0x00) — 4 more ushort values
/// follow the type byte before the next real opcode or text run starts.
/// Purpose of the 4 values is unknown; all four are plausible-looking
/// small numbers in the observed data (tens to low hundreds), not
/// obviously offsets or IDs into anything else in this project.</summary>
public sealed class Unknown0DUnknown1Opcode : IUnknown0DOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown0D;
	public Unknown0DOption Option => Unknown0DOption.Unknown1;

	public ushort Unknown1 { get; set; }
	public ushort Unknown2 { get; set; }
	public ushort Unknown3 { get; set; }
	public ushort Unknown4 { get; set; }
}
