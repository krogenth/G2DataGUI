using G2DataGUI.Domain.Enemies;
using G2DataGUI.Export.Abstractions;

namespace G2DataGUI.Export.Enemies;

/// <summary>
/// Matches the original output exactly: one row per move (5 rows per
/// enemy), no enemy-identifying column. Not ideal for re-reading the CSV
/// back later, but this is what the old MovesetCSVHeader/GenerateMovesetCSV
/// produced — not a gap introduced by this port.
/// </summary>
public sealed class EnemyMovesetCsvExporter(EnemyMoveCsvExporter moveExporter) : ICsvExporter<Enemy>
{
	public string Header => moveExporter.Header;

	public void WriteRow(TextWriter writer, Enemy item)
	{
		foreach (EnemyMove move in item.Moveset.Moves)
		{
			moveExporter.WriteLine(writer, move);
		}
	}
}
