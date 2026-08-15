namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>Completely unknown, may be 1 or 4 parameters — original reader
/// only ever consumed 1 byte, so ported as 1.</summary>
public sealed class Unknown07Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown07;

	public byte Unknown1 { get; set; }
}
