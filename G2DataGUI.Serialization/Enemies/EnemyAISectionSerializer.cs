using G2DataGUI.Domain.Enemies;

namespace G2DataGUI.Serialization.Enemies;

public static class EnemyAISectionSerializer
{
	public static EnemyAISection Read(Stream stream)
	{
		List<EnemyAI> conditions = [];

		for (int i = 0; i < EnemyAISection.ConditionsPerSection; i++)
		{
			conditions.Add(EnemyAISerializer.Read(stream));
		}

		return new EnemyAISection { Conditions = conditions };
	}

	public static void Write(Stream stream, EnemyAISection section)
	{
		foreach (EnemyAI ai in section.Conditions)
		{
			EnemyAISerializer.Write(stream, ai);
		}
	}
}
