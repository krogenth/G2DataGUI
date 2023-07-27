using System.IO;
namespace G2DataGUI.Common.Data.Levelups;

public class CharacterLevelups
{
	public string CharacterName { get; private set; }

	public static int LevelupsPerCharacter { get; } = 99;

    public LevelupStats[] Levelups { get; set; } = new LevelupStats[LevelupsPerCharacter];

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
}
