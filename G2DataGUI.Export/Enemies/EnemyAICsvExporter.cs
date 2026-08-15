using G2DataGUI.Domain.Enemies;

namespace G2DataGUI.Export.Enemies;

public sealed class EnemyAICsvExporter
{
	public string Header => "Condition,Move #1 Chance,Move #2 Chance,Move #3 Chance,Move #4 Chance,Move #5 Chance";

	public void WriteFields(TextWriter writer, EnemyAI ai) =>
		writer.Write($"{ai.Condition},{ai.Move1Chance},{ai.Move2Chance},{ai.Move3Chance},{ai.Move4Chance},{ai.Move5Chance}");
}
