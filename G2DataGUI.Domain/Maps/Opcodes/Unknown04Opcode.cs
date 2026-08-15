namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Completely unknown, likely 1 parameter.</summary>
public sealed class Unknown04Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown04;

	public byte Unknown1 { get; set; }
}
