using G2DataGUI.Domain.Enemies;
using G2DataGUI.Serialization.Enemies;

namespace G2DataGUI.Repositories.Enemies;

public sealed class EnemyRepository(string directory) : DirectoryScanRepository<Enemy>("Enemies", directory)
{
	protected override IEnumerable<Enemy> ReadFile(Stream stream, string filePath)
	{
		(var stats, var aiSection, var moveset) = EnemyEntrySerializer.Read(stream, isSecond: false);

		yield return new Enemy
		{
			Stats = stats,
			AISection = aiSection,
			Moveset = moveset,
			IsSecond = false,
			FilePath = filePath
		};

		if (EnemyEntrySerializer.HasSecondEntry(stream))
		{
			(var stats2, var aiSection2, var moveset2) = EnemyEntrySerializer.Read(stream, isSecond: true);

			yield return new Enemy
			{
				Stats = stats2,
				AISection = aiSection2,
				Moveset = moveset2,
				IsSecond = true,
				FilePath = filePath
			};
		}
	}

	protected override void WriteEntry(Enemy item) =>
		EnemyEntrySerializer.Write(item.FilePath, item.IsSecond, item.Stats, item.AISection, item.Moveset);
}
