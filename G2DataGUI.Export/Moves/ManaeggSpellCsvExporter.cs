using G2DataGUI.Domain.Moves;
using G2DataGUI.Repositories.Moves;

namespace G2DataGUI.Export.Moves;

public sealed class ManaeggSpellCsvExporter(MoveRepository moves)
{
	public string Header => "Spell,Starting Level,Required Level,Unknown #1";

	public void WriteFields(TextWriter writer, ManaeggSpell spell)
	{
		string moveName = moves.Find(spell.SpellOffset)?.Name ?? $"<unknown {spell.SpellOffset}>";

		writer.Write($"{moveName},{spell.StartingLevel},{spell.RequiredLevel},{spell.Unknown1}");
	}
}
