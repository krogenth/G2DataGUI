using G2DataGUI.Common.Data.Common;
using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.IO.Streams;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace G2DataGUI.Common.Data.Bosses;

/// <summary>
/// Defines explicit boss instance within the game(non-overworld enemies or field bosses)
/// bosses follow similar rules to enemies, but have an additional zXX_act.dat file
/// which defines additional movesets for the boss.
/// </summary>
public class Boss : BaseContainer
{
	public EnemyStats Stats { get; private set; } = new();
	public EnemyAISection AISection { get; private set; } = new();
	/// <summary>
	/// Additional movesets defined in corresponding _act.dat file
	/// </summary>
	public ObservableCollection<EnemyMoveset> Movesets { get; private set; } = [];

	public bool IsSecond { get; set; } = false;
	public string Filename { get; set; } = string.Empty;

	public static int FirstStatsPointerOffset => 0x34;
	public static int SecondStatsPointerOffset => 0x44;
	public static int FirstMovesPointerOffset => 0x3C;
	public static int SecondMovesPointerOffset => 0x4C;

	public static string BossCSVHeader =>
		$"{EnemyStats.CSVHeader},{EnemyAISection.CSVHeader}";

	public static string MovesetCSVHeader =>
		$"{EnemyMoveset.CSVHeader}";

	public static Boss ReadBoss(Stream reader, string file, bool isSecond = false)
    {
		// first boss instance offset is always 0x34
		// second boss instance offset is always 0x44
		reader.Seek(isSecond ? SecondStatsPointerOffset : FirstStatsPointerOffset, SeekOrigin.Begin);
		reader.Seek(reader.ReadRawInt(), SeekOrigin.Begin);

        Boss boss = new()
        {
			Stats = EnemyStats.ReadEnemyStats(reader),
            AISection = EnemyAISection.ReadEnemyAISection(reader),
			IsSecond = isSecond,
			Filename = file,
		};

		// first boss moveset instance offset is always 0x3C
		// second boss moveset instance offset is always 0x4C
		reader.Seek(isSecond ? SecondMovesPointerOffset : FirstMovesPointerOffset, SeekOrigin.Begin);
		reader.Seek(reader.ReadRawInt(), SeekOrigin.Begin);
		boss.Movesets.Add(EnemyMoveset.ReadEnemyMoveset(reader));

        return boss;
    }

	public void WriteBoss()
	{
		// we need the file to write to
		if (string.IsNullOrEmpty(Filename))
		{
			return;
		}

		using FileStream writer = File.Open(Filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);

		// first boss instance offset is always 0x34
		// second boss instance offset is always 0x44
		writer.Seek(IsSecond ? SecondStatsPointerOffset : FirstStatsPointerOffset, SeekOrigin.Begin);
		writer.Seek(writer.ReadRawInt(), SeekOrigin.Begin);

		Stats.WriteEnemyStats(writer);
		AISection.WriteEnemyAISection(writer);

		writer.Seek(IsSecond ? SecondMovesPointerOffset : FirstMovesPointerOffset, SeekOrigin.Begin);
		writer.Seek(writer.ReadRawInt(), SeekOrigin.Begin);

		Movesets.First().WriteEnemyMoveset(writer);
	}

	public static bool FileHasSecondBoss(Stream reader)
    {
		// second boss instance offset is always at 0x44
		long position = reader.Position;
		reader.Seek(SecondStatsPointerOffset, SeekOrigin.Begin);
		bool hasSecond = reader.ReadRawInt() > 0;
		reader.Seek(position, SeekOrigin.Begin);
		return hasSecond;
	}

	public void GenerateBossCSV(StreamWriter writer)
	{
		Stats.GenerateCSV(writer);
		writer.Write(',');
		AISection.GenerateCSV(writer);
		writer.WriteLine();
	}

	public void GenerateMovesetCSV(StreamWriter writer)
	{
		foreach (var moveset in Movesets)
		{
			moveset.GenerateCSV(writer);
		}
	}
}
