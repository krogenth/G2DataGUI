using G2DataGUI.Domain.Enemies;

namespace G2DataGUI.Export.Enemies;

public sealed class EnemyAISectionCsvExporter(EnemyAICsvExporter aiExporter)
{
	public string Header =>
		string.Join(",", Enumerable.Repeat(aiExporter.Header, EnemyAISection.ConditionsPerSection));

	public void WriteFields(TextWriter writer, EnemyAISection section)
	{
		for (int i = 0; i < section.Conditions.Count; i++)
		{
			if (i > 0)
			{
				writer.Write(',');
			}

			aiExporter.WriteFields(writer, section.Conditions[i]);
		}
	}
}
