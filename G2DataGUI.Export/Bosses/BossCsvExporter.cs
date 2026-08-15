using G2DataGUI.Domain.Bosses;
using G2DataGUI.Export.Abstractions;
using G2DataGUI.Export.Enemies;

namespace G2DataGUI.Export.Bosses;

public sealed class BossCsvExporter(EnemyStatsCsvExporter statsExporter, EnemyAISectionCsvExporter aiExporter)
	: ICsvExporter<Boss>
{
	public string Header => $"{statsExporter.Header},{aiExporter.Header}";

	public void WriteRow(TextWriter writer, Boss item)
	{
		statsExporter.WriteFields(writer, item.Stats);
		writer.Write(',');
		aiExporter.WriteFields(writer, item.AISection);
		writer.WriteLine();
	}
}
