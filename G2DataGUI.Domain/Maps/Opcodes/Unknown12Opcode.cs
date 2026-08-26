namespace G2DataGUI.Domain.Maps.Opcodes;

/// <summary>
/// Newly identified, not part of the original port, and lower confidence
/// than Unknown0E/Unknown0F — only 1 occurrence found across ~350KB of
/// real dialogue data across 7 of the game's largest maps
/// (`12 1d ff...`), which resynced correctly as a zero-parameter opcode
/// (0x1D immediately after, with a valid Pause sub-option). A single
/// data point can't rule out this actually needing parameter bytes that
/// simply happened to look like a valid Pause by coincidence — treat
/// this one as worth re-checking against more data before relying on it,
/// unlike Unknown0E/Unknown0F which had double-digit occurrence counts
/// all agreeing.
/// </summary>
public sealed class Unknown12Opcode : IMapOpcode
{
	public DialogueOpcode Opcode => DialogueOpcode.Unknown12;
}
