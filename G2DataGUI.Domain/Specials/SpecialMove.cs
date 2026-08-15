namespace G2DataGUI.Domain.Specials;

public sealed class SpecialMove
{
	/// <summary>Index into Moves — same cross-reference pattern as
	/// ManaeggSpell.SpellOffset, resolved via MoveRepository.Find in the exporter.</summary>
	public byte MoveOffset { get; set; }

	public byte StartingLevel { get; set; }
	public ushort StoryFlag { get; set; }
}
