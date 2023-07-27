using System.IO;
namespace G2DataGUI.Common.Data.Levelups;

public class CharacterLevelups
{
	public static int LevelupsPerCharacter { get; } = 99;

	// used for UI list rendering
	public string CharacterName { get; private set; }

	public LevelupStats[] Levelups { get; set; } = new LevelupStats[LevelupsPerCharacter];

	public static string CSVHeader =>
		$"Character,{LevelupStats.CSVHeader}";

	public static CharacterLevelups ReadCharacterLevelups(Stream reader, string characterName)
	{
		CharacterLevelups levelups = new()
		{
			CharacterName = characterName,
		};
		for (int index = 0; index < LevelupsPerCharacter; index++)
		{
			levelups.Levelups[index] = LevelupStats.ReadLevelupStats(reader, index+1);
		}

		return levelups;
	}

	public void WriteCharacterLevelups(Stream writer)
	{
		foreach (var levelups in Levelups)
		{
			levelups.WriteLevelupStats(writer);
		}
	}

	public void GenerateCSV(StreamWriter writer)
	{
		foreach(var levelup in Levelups)
		{
			writer.Write($"{CharacterName},");
			levelup.GenerateCSV(writer);
			writer.WriteLine();
		}
	}
}
