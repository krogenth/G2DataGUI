namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Completely unknown, seems to have 1 parameter.</summary>
public sealed class Unknown1COpcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown1C;

	public byte Unknown1 { get; set; }
}
