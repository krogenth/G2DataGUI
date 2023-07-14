using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class Enemy : BaseContainer
{
    private FixedLengthName _name;
    public EnemyStats Stats { get; private set; }
    public EnemyAISection AISection { get; private set; }
    public EnemyMoveset Moveset { get; private set; }

    public bool IsSecond { get; set; }
    public string Filename { get; set; }

    public string Name
    {
        get => _name.Name;
        set {
            _name.Name = value;
            NotifyPropertyChanged(nameof(Name));
        }
    }
    public int MaxNameLength { get => FixedLengthName.MaxLength; }

    public static int FirstStatsPointerOffset { get; } = 0x34;
    public static int SecondStatsPointerOffset { get; } = 0x44;
    public static int FirstMovesPointerOffset { get; } = 0x3C;
    public static int SecondMovesPointerOffset { get; } = 0x4C;

    public static Enemy ReadEnemy(Stream reader, string file, bool isSecond = false)
    {
        // first enemy instance offset is always 0x34
        // second enemy instance offset is always 0x44
        reader.Seek(isSecond ? SecondStatsPointerOffset : FirstStatsPointerOffset, SeekOrigin.Begin);
        reader.Seek(reader.ReadRawInt(), SeekOrigin.Begin);

        Enemy enemy = new()
        {
            _name = FixedLengthName.ReadFixedLengthName(reader),
            Stats = EnemyStats.ReadEnemyStats(reader),
            AISection = EnemyAISection.ReadEnemyAISection(reader),
            IsSecond = isSecond,
            Filename = file,
        };

        // first enemy moveset instance offset is always 0x3C
        // second enemy moveset instance offset is always 0x4C
        reader.Seek(isSecond ? SecondMovesPointerOffset : FirstMovesPointerOffset, SeekOrigin.Begin);
        reader.Seek(reader.ReadRawInt(), SeekOrigin.Begin);
        enemy.Moveset = EnemyMoveset.ReadEnemyMoveset(reader);

        return enemy;
    }

    public void WriteEnemy()
    {
        // we need the file to write to
        if (Filename.Length <= 0)
		{
			return;
		}

		using FileStream writer = File.Open(Filename, FileMode.OpenOrCreate, FileAccess.ReadWrite);

        // first enemy instance offset is always 0x34
        // second enemy instance offset is always 0x44
        writer.Seek(IsSecond ? SecondStatsPointerOffset : FirstStatsPointerOffset, SeekOrigin.Begin);
        writer.Seek(writer.ReadRawInt(), SeekOrigin.Begin);

        _name.WriteFixedLengthName(writer);
        Stats.WriteEnemyStats(writer);
        AISection.WriteEnemyAISection(writer);

        // first enemy moveset instance offset is always 0x3C
        // second enemy moveset instance offset is always 0x4C
        writer.Seek(IsSecond ? SecondMovesPointerOffset : FirstMovesPointerOffset, SeekOrigin.Begin);
        writer.Seek(writer.ReadRawInt(), SeekOrigin.Begin);

        Moveset.WriteEnemyMoveset(writer);
    }

    public static bool FileHasSecondEnemy(Stream reader)
    {
        // second enemy instance offset is always at 0x44
		long position = reader.Position;
        reader.Seek(SecondStatsPointerOffset, SeekOrigin.Begin);
        bool hasSecond = reader.ReadRawInt() > 0;
        reader.Seek(position, SeekOrigin.Begin);
        return hasSecond;
    }
}
