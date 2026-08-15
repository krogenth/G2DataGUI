using G2DataGUI.Domain.Enemies;
using G2DataGUI.Export.Abstractions;

namespace G2DataGUI.Export.Enemies;

public sealed class EnemyCsvExporter(EnemyStatsCsvExporter statsExporter, EnemyAISectionCsvExporter aiExporter)
	: ICsvExporter<Enemy>
{
	public string Header => $"{statsExporter.Header},{aiExporter.Header}";

	public void WriteRow(TextWriter writer, Enemy item)
	{
		statsExporter.WriteFields(writer, item.Stats);
		writer.Write(',');
		aiExporter.WriteFields(writer, item.AISection);
		writer.WriteLine();
	}
}
