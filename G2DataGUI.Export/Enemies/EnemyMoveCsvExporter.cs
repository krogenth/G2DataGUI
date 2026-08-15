using G2DataGUI.Domain.Enemies;

namespace G2DataGUI.Export.Enemies;

public sealed class EnemyMoveCsvExporter(EnemyMoveStatsCsvExporter statsExporter)
{
	public string Header => $"Name,{statsExporter.Header}";

	public void WriteLine(TextWriter writer, EnemyMove move)
	{
		writer.Write($"{move.Name},");
		statsExporter.WriteFields(writer, move.Stats);
		writer.WriteLine();
	}
}
