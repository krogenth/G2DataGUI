using G2DataGUI.Domain.Bosses;
using G2DataGUI.Domain.Enemies;
using G2DataGUI.Serialization.Enemies;

namespace G2DataGUI.Repositories.Bosses;

public sealed class BossRepository(string directory) : DirectoryScanRepository<Boss>("Bosses", directory)
{
	protected override IEnumerable<Boss> ReadFile(Stream stream, string filePath)
	{
		(var stats, var aiSection, var moveset) = EnemyEntrySerializer.Read(stream, isSecond: false);

		yield return new Boss
		{
			Stats = stats,
			AISection = aiSection,
			Movesets = [moveset],
			IsSecond = false,
			FilePath = filePath
		};

		if (EnemyEntrySerializer.HasSecondEntry(stream))
		{
			(var stats2, var aiSection2, var moveset2) = EnemyEntrySerializer.Read(stream, isSecond: true);

			yield return new Boss
			{
				Stats = stats2,
				AISection = aiSection2,
				Movesets = [moveset2],
				IsSecond = true,
				FilePath = filePath
			};
		}
	}

	protected override void WriteEntry(Boss item) =>
		EnemyEntrySerializer.Write(
			item.FilePath,
			item.IsSecond,
			item.Stats,
			item.AISection,
			item.Movesets.Count > 0 ? item.Movesets[0] : new EnemyMoveset());
}
