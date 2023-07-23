using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyAISection
{
    public EnemyAI AI1 { get; set; }
    public EnemyAI AI2 { get; set; }
    public EnemyAI AI3 { get; set; }
    public EnemyAI AI4 { get; set; }
    public EnemyAI AI5 { get; set; }

	public static string CSVHeader =>
		$"{EnemyAI.CSVHeader},{EnemyAI.CSVHeader},{EnemyAI.CSVHeader},{EnemyAI.CSVHeader},{EnemyAI.CSVHeader}";

    public static EnemyAISection ReadEnemyAISection(Stream reader)
    {
		EnemyAISection section = new()
		{
			AI1 = EnemyAI.ReadEnemyAI(reader),
			AI2 = EnemyAI.ReadEnemyAI(reader),
			AI3 = EnemyAI.ReadEnemyAI(reader),
			AI4 = EnemyAI.ReadEnemyAI(reader),
			AI5 = EnemyAI.ReadEnemyAI(reader)
		};

		return section;
    }

    public void WriteEnemyAISection(Stream writer)
    {
        AI1.WriteEnemyAI(writer);
        AI2.WriteEnemyAI(writer);
        AI3.WriteEnemyAI(writer);
        AI4.WriteEnemyAI(writer);
        AI5.WriteEnemyAI(writer);
    }

	public void GenerateCSV(StreamWriter writer)
	{
		AI1.GenerateCSV(writer);
		writer.Write(',');
		AI2.GenerateCSV(writer);
		writer.Write(',');
		AI3.GenerateCSV(writer);
		writer.Write(',');
		AI4.GenerateCSV(writer);
		writer.Write(',');
		AI5.GenerateCSV(writer);
	}
}
