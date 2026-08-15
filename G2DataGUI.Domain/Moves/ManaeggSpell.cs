namespace G2DataGUI.Domain.Moves;

public sealed class ManaeggSpell
{
	/// <summary>Index into the Moves table. Resolved to a name at export
	/// time via a MoveRepository lookup — see ManaeggSpellCsvExporter.</summary>
	public byte SpellOffset { get; set; }
	public byte StartingLevel { get; set; }
	public byte RequiredLevel { get; set; }
	public byte Unknown1 { get; set; }
}
