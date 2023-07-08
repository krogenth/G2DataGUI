using G2DataGUI.Common.Data.Common;
using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.IO.Streams;
using System.Collections.Generic;
using System.IO;

namespace G2DataGUI.Common.Data.Bosses;

public class Boss
{
    public FixedLengthName Name;

    public EnemyStats Stats;
    public EnemyAISection AIs;
    public List<EnemyMoveset> Movesets = new();

    private Boss()
    {
        Name = new FixedLengthName();
    }

    public bool IsSecond { get; set; }
    public string File { get; set; } = "";

    public static Boss ReadBoss(BinaryReader binReader, string file, bool isSecond = false)
    {
        // first boss instance offset is always 0x34
        // second boss instance offset is always 0x44
        if (isSecond)
        {
            binReader.BaseStream.Seek(0x44, SeekOrigin.Begin);
        }
        else
        {
            binReader.BaseStream.Seek(0x34, SeekOrigin.Begin);
        }

        Boss boss = new()
        {
            File = file,
            IsSecond = isSecond,
            Name = FixedLengthName.ReadFixedLengthName(binReader.BaseStream),
            AIs = EnemyAISection.ReadEnemyAISection(binReader.BaseStream),
        };

        // first boss moveset instance offset is always 0x3C
        // second boss moveset instance offset is always 0x4C
        if (isSecond)
        {
            binReader.BaseStream.Seek(0x4C, SeekOrigin.Begin);
        }
        else
        {
            binReader.BaseStream.Seek(0x3C, SeekOrigin.Begin);
        }

        binReader.BaseStream.Seek(binReader.ReadInt32(), SeekOrigin.Begin);

        boss.Movesets.Add(EnemyMoveset.ReadEnemyMoveset(binReader.BaseStream));

        return boss;
    }

    public static bool FileHasSecondBoss(BinaryReader binReader)
    {
        // second boss instance offset is always at 0x44
        return binReader.ReadInt32() > 0;
    }
}