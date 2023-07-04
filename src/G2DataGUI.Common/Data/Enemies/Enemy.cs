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
    public string File { get; set; } = "";

    public string Name { get => _name.Name; set { _name.Name = value; NotifyPropertyChanged(nameof(Name)); } }
    public int MaxNameLength { get => _name.MaxLength; }

    public static int FirstStatsPointerOffset { get; } = 0x34;
    public static int SecondStatsPointerOffset { get; } = 0x34;
    public static int FirstMovesPointerOffset { get; } = 0x3C;
    public static int SecondMovesPointerOffset { get; } = 0x4C;

    public static Enemy ReadEnemy(Stream reader, string file, bool isSecond = false)
    {
        Enemy enemy = new Enemy();
        enemy.File = file;
        enemy.IsSecond = isSecond;

        // first enemy instance offset is always 0x34
        // second enemy instance offset is always 0x44
        if (isSecond) reader.Seek(SecondStatsPointerOffset, SeekOrigin.Begin);
        else reader.Seek(FirstStatsPointerOffset, SeekOrigin.Begin);
        reader.Seek(reader.ReadRawInt(), SeekOrigin.Begin);

        FixedLengthName? name = reader.ReadStruct<FixedLengthName>();
        if (name != null) enemy._name = (FixedLengthName)name;

        enemy.Stats = EnemyStats.ReadEnemyStats(reader);
        enemy.AISection = EnemyAISection.ReadEnemyAISection(reader);

        // first enemy moveset instance offset is always 0x3C
        // second enemy moveset instance offset is always 0x4C
        if (isSecond) reader.Seek(SecondMovesPointerOffset, SeekOrigin.Begin);
        else reader.Seek(FirstMovesPointerOffset, SeekOrigin.Begin);
        reader.Seek(reader.ReadRawInt(), SeekOrigin.Begin);

        enemy.Moveset = EnemyMoveset.ReadEnemyMoveset(reader);

        return enemy;
    }

    public void WriteEnemy()
    {
        // we need the file to write to
        if (File.Length <= 0) return;

        using (FileStream writer = new FileStream(File, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            if (IsSecond) writer.Seek(SecondStatsPointerOffset, SeekOrigin.Begin);
            else writer.Seek(FirstStatsPointerOffset, SeekOrigin.Begin);
            writer.Seek(writer.ReadRawInt(), SeekOrigin.Begin);

            writer.WriteStruct(_name);
            Stats.WriteEnemyStats(writer);
            AISection.WriteEnemyAISection(writer);

            if (IsSecond) writer.Seek(SecondMovesPointerOffset, SeekOrigin.Begin);
            else writer.Seek(FirstMovesPointerOffset, SeekOrigin.Begin);
            writer.Seek(writer.ReadRawInt(), SeekOrigin.Begin);

            Moveset.WriteEnemyMoveset(writer);
        }
    }

    public static bool FileHasSecondEnemy(Stream reader)
    {
        // second enemy instance offset is always at 0x44
        if (reader == null) return false;
        long position = reader.Position;
        reader.Seek(0x44, SeekOrigin.Begin);
        bool hasSecond = reader.ReadRawInt() > 0;
        reader.Seek(position, SeekOrigin.Begin);
        return hasSecond;
    }
}