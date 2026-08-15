namespace G2DataGUI.Domain.Maps.Opcodes.Unknown10;

public sealed class Unknown10Unknown1Opcode : IUnknown10Opcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown10;
	public Unknown10Option Option => Unknown10Option.Unknown1;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public short MapID { get; set; }
	public byte Unknown3 { get; set; }
}
