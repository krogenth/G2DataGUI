namespace G2DataGUI.Domain.Maps.Opcodes.Pause;

/// <summary>Pauses text rendering for a time (tick based?) — sub-dispatches
/// on a second byte (PauseOption). No recursion, unlike TextBox/Highlight.</summary>
public interface IPauseOpcode : IMapOpcode
{
	PauseOption Option { get; }
}
