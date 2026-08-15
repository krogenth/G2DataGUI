using G2DataGUI.Domain.Bosses;
using G2DataGUI.Domain.Enemies;
using G2DataGUI.Export.Abstractions;
using G2DataGUI.Export.Enemies;

namespace G2DataGUI.Export.Bosses;

/// <summary>
/// See the doc comment on EnemyMovesetCsvExporter — same shape,
/// one row per move with no boss-identifying column, matching the original.
/// </summary>
public sealed class BossMovesetCsvExporter(EnemyMoveCsvExporter moveExporter) : ICsvExporter<Boss>
{
	public string Header => moveExporter.Header;

	public void WriteRow(TextWriter writer, Boss item)
	{
		foreach (EnemyMoveset moveset in item.Movesets)
		{
			foreach (EnemyMove move in moveset.Moves)
			{
				moveExporter.WriteLine(writer, move);
			}
		}
	}
}
