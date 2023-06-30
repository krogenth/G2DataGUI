using G2DataGUI.Common.Data.Common;
using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.IO.Streams;
using System.Collections.Generic;
using System.IO;

namespace G2DataGUI.Common.Data.Bosses;

public class Boss
{
    public FixedLengthName name;

    public EnemyStats stats;
    public EnemyAISection AIs;
    public List<EnemyMoveset> movesets = new List<EnemyMoveset>();

    private Boss()
    {
        name = new FixedLengthName();
    }

    public bool isSecond { get; set; }
    public string file { get; set; } = "";

    public static Boss ReadBoss(BinaryReader binReader, string file, bool isSecond = false)
    {
        Boss boss = new Boss();
        boss.file = file;
        boss.isSecond = isSecond;

        // first boss instance offset is always 0x34
        // second boss instance offset is always 0x44
        if (isSecond) binReader.BaseStream.Seek(0x44, SeekOrigin.Begin);
        else binReader.BaseStream.Seek(0x34, SeekOrigin.Begin);
        binReader.BaseStream.Seek(binReader.ReadInt32(), SeekOrigin.Begin);

        FixedLengthName? name = binReader.BaseStream.ReadStruct<FixedLengthName>();
        if (name != null) boss.name = (FixedLengthName)name;

        boss.AIs = EnemyAISection.ReadEnemyAISection(binReader.BaseStream);

        // first boss moveset instance offset is always 0x3C
        // second boss moveset instance offset is always 0x4C
        if (isSecond) binReader.BaseStream.Seek(0x4C, SeekOrigin.Begin);
        else binReader.BaseStream.Seek(0x3C, SeekOrigin.Begin);
        binReader.BaseStream.Seek(binReader.ReadInt32(), SeekOrigin.Begin);

        boss.movesets.Add(EnemyMoveset.ReadEnemyMoveset(binReader.BaseStream));

        return boss;
    }

    public static bool FileHasSecondBoss(BinaryReader binReader)
    {
        // second boss instance offset is always at 0x44
        return binReader.ReadInt32() > 0;
    }
}