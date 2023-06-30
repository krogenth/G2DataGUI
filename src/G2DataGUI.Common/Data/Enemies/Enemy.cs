using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class Enemy
{
    private FixedLengthName _name;

    private EnemyStats _stats;
    private EnemyAISection _AIs;
    private EnemyMoveset _moveset;

    public bool IsSecond { get; set; }
    public string File { get; set; } = "";

    public static Enemy ReadEnemy(Stream reader, string file, bool isSecond = false)
    {
        Enemy enemy = new Enemy();
        enemy.File = file;
        enemy.IsSecond = isSecond;

        // first enemy instance offset is always 0x34
        // second enemy instance offset is always 0x44
        if (isSecond) reader.Seek(0x44, SeekOrigin.Begin);
        else reader.Seek(0x34, SeekOrigin.Begin);
        reader.Seek(reader.ReadInt(), SeekOrigin.Begin);

        FixedLengthName? name = reader.ReadStruct<FixedLengthName>();
        if (name != null) enemy._name = (FixedLengthName)name;

        enemy._AIs = EnemyAISection.ReadEnemyAISection(reader);

        // first enemy moveset instance offset is always 0x3C
        // second enemy moveset instance offset is always 0x4C
        if (isSecond) reader.Seek(0x4C, SeekOrigin.Begin);
        else reader.Seek(0x3C, SeekOrigin.Begin);
        reader.Seek(reader.ReadInt(), SeekOrigin.Begin);

        enemy._moveset = EnemyMoveset.ReadEnemyMoveset(reader);

        return enemy;
    }

    public static bool FileHasSecondEnemy(Stream reader)
    {
        // second enemy instance offset is always at 0x44
        if (reader == null) return false;
        long position = reader.Position;
        reader.Seek(0x44, SeekOrigin.Begin);
        bool hasSecond = reader.ReadInt() > 0;
        reader.Seek(position, SeekOrigin.Begin);
        return hasSecond;
    }
}