using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class Manaegg : BaseContainer
{
    private ManaeggSpell[] _spells = new ManaeggSpell[18];

    public static Manaegg ReadManaegg(Stream reader)
    {
        Manaegg manaegg = new Manaegg();
        for (int index = 0; index < manaegg._spells.Length; index++)
        {
            ManaeggSpell? spell = reader.ReadStruct<ManaeggSpell>();
            if (spell == null) return null;
            manaegg._spells[index] = spell.Value;
        }

        return manaegg;
    }

    public void WriteManaegg(Stream writer)
    {
        foreach (var spell in _spells)
        {
            writer.WriteStruct(spell);
        }
    }

    public int NumSpells { get => _spells.Length; }

    public byte Spell1Offset { get => _spells[0].SpellOffset; set => _spells[0].SpellOffset = value; }
    public byte Spell1StartingLevel { get => _spells[0].StartingLevel; set => _spells[0].StartingLevel = value; }
    public byte Spell1RequiredLevel { get => _spells[0].RequiredLevel; set => _spells[0].RequiredLevel = value; }

    public byte Spell2Offset { get => _spells[1].SpellOffset; set => _spells[1].SpellOffset = value; }
    public byte Spell2StartingLevel { get => _spells[1].StartingLevel; set => _spells[1].StartingLevel = value; }
    public byte Spell2RequiredLevel { get => _spells[1].RequiredLevel; set => _spells[1].RequiredLevel = value; }

    public byte Spell3Offset { get => _spells[2].SpellOffset; set => _spells[2].SpellOffset = value; }
    public byte Spell3StartingLevel { get => _spells[2].StartingLevel; set => _spells[2].StartingLevel = value; }
    public byte Spell3RequiredLevel { get => _spells[2].RequiredLevel; set => _spells[2].RequiredLevel = value; }

    public byte Spell4Offset { get => _spells[3].SpellOffset; set => _spells[3].SpellOffset = value; }
    public byte Spell4StartingLevel { get => _spells[3].StartingLevel; set => _spells[3].StartingLevel = value; }
    public byte Spell4RequiredLevel { get => _spells[3].RequiredLevel; set => _spells[3].RequiredLevel = value; }

    public byte Spell5Offset { get => _spells[4].SpellOffset; set => _spells[4].SpellOffset = value; }
    public byte Spell5StartingLevel { get => _spells[4].StartingLevel; set => _spells[4].StartingLevel = value; }
    public byte Spell5RequiredLevel { get => _spells[4].RequiredLevel; set => _spells[4].RequiredLevel = value; }

    public byte Spell6Offset { get => _spells[5].SpellOffset; set => _spells[5].SpellOffset = value; }
    public byte Spell6StartingLevel { get => _spells[5].StartingLevel; set => _spells[5].StartingLevel = value; }
    public byte Spell6RequiredLevel { get => _spells[5].RequiredLevel; set => _spells[5].RequiredLevel = value; }

    public byte Spell7Offset { get => _spells[6].SpellOffset; set => _spells[6].SpellOffset = value; }
    public byte Spell7StartingLevel { get => _spells[6].StartingLevel; set => _spells[6].StartingLevel = value; }
    public byte Spell7RequiredLevel { get => _spells[6].RequiredLevel; set => _spells[6].RequiredLevel = value; }

    public byte Spell8Offset { get => _spells[7].SpellOffset; set => _spells[7].SpellOffset = value; }
    public byte Spell8StartingLevel { get => _spells[7].StartingLevel; set => _spells[7].StartingLevel = value; }
    public byte Spell8RequiredLevel { get => _spells[7].RequiredLevel; set => _spells[7].RequiredLevel = value; }

    public byte Spell9Offset { get => _spells[8].SpellOffset; set => _spells[8].SpellOffset = value; }
    public byte Spell9StartingLevel { get => _spells[8].StartingLevel; set => _spells[8].StartingLevel = value; }
    public byte Spell9RequiredLevel { get => _spells[8].RequiredLevel; set => _spells[8].RequiredLevel = value; }

    public byte Spell10Offset { get => _spells[9].SpellOffset; set => _spells[9].SpellOffset = value; }
    public byte Spell10StartingLevel { get => _spells[9].StartingLevel; set => _spells[9].StartingLevel = value; }
    public byte Spell10RequiredLevel { get => _spells[9].RequiredLevel; set => _spells[9].RequiredLevel = value; }

    public byte Spell11Offset { get => _spells[10].SpellOffset; set => _spells[10].SpellOffset = value; }
    public byte Spell11StartingLevel { get => _spells[10].StartingLevel; set => _spells[10].StartingLevel = value; }
    public byte Spell11RequiredLevel { get => _spells[10].RequiredLevel; set => _spells[10].RequiredLevel = value; }

    public byte Spell12Offset { get => _spells[11].SpellOffset; set => _spells[11].SpellOffset = value; }
    public byte Spell12StartingLevel { get => _spells[11].StartingLevel; set => _spells[11].StartingLevel = value; }
    public byte Spell12RequiredLevel { get => _spells[11].RequiredLevel; set => _spells[11].RequiredLevel = value; }

    public byte Spell13Offset { get => _spells[12].SpellOffset; set => _spells[12].SpellOffset = value; }
    public byte Spell13StartingLevel { get => _spells[12].StartingLevel; set => _spells[12].StartingLevel = value; }
    public byte Spell13RequiredLevel { get => _spells[12].RequiredLevel; set => _spells[12].RequiredLevel = value; }

    public byte Spell14Offset { get => _spells[13].SpellOffset; set => _spells[13].SpellOffset = value; }
    public byte Spell14StartingLevel { get => _spells[13].StartingLevel; set => _spells[13].StartingLevel = value; }
    public byte Spell14RequiredLevel { get => _spells[13].RequiredLevel; set => _spells[13].RequiredLevel = value; }

    public byte Spell15Offset { get => _spells[14].SpellOffset; set => _spells[14].SpellOffset = value; }
    public byte Spell15StartingLevel { get => _spells[14].StartingLevel; set => _spells[14].StartingLevel = value; }
    public byte Spell15RequiredLevel { get => _spells[14].RequiredLevel; set => _spells[14].RequiredLevel = value; }

    public byte Spell16Offset { get => _spells[15].SpellOffset; set => _spells[15].SpellOffset = value; }
    public byte Spell16StartingLevel { get => _spells[15].StartingLevel; set => _spells[15].StartingLevel = value; }
    public byte Spell16RequiredLevel { get => _spells[15].RequiredLevel; set => _spells[15].RequiredLevel = value; }

    public byte Spell17Offset { get => _spells[16].SpellOffset; set => _spells[16].SpellOffset = value; }
    public byte Spell17StartingLevel { get => _spells[16].StartingLevel; set => _spells[16].StartingLevel = value; }
    public byte Spell17RequiredLevel { get => _spells[16].RequiredLevel; set => _spells[16].RequiredLevel = value; }

    public byte Spell18Offset { get => _spells[17].SpellOffset; set => _spells[17].SpellOffset = value; }
    public byte Spell18StartingLevel { get => _spells[17].StartingLevel; set => _spells[17].StartingLevel = value; }
    public byte Spell18RequiredLevel { get => _spells[17].RequiredLevel; set => _spells[17].RequiredLevel = value; }
}