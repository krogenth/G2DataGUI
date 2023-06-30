using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class SpecialSet
{
    private SpecialMove[] _specials = new SpecialMove[6];

    public static SpecialSet ReadSpecialSet(Stream reader)
    {
        SpecialSet specialSet = new SpecialSet();
        for (int index = 0; index < specialSet._specials.Length; index++)
        {
            SpecialMove? special = reader.ReadStruct<SpecialMove>();
            if (special == null) return null;
            specialSet._specials[index] = special.Value;
        }

        return specialSet;
    }

    public void WriteSpecialSet(Stream writer)
    {
        foreach (SpecialMove specialMove in _specials)
        {
            writer.WriteStruct(specialMove);
        }
    }

    public int NumSpecials { get => _specials.Length; }

    public byte Special1Offset { get => _specials[0].MoveOffset; set => _specials[0].MoveOffset = value; }
    public byte Special1StartingLevel { get => _specials[0].StartingLevel; set => _specials[0].StartingLevel = value; }
    public ushort Special1StoryFlag { get => _specials[0].StoryFlag; set => _specials[0].StoryFlag = value; }

    public byte Special2Offset { get => _specials[1].MoveOffset; set => _specials[1].MoveOffset = value; }
    public byte Special2StartingLevel { get => _specials[1].StartingLevel; set => _specials[1].StartingLevel = value; }
    public ushort Special2StoryFlag { get => _specials[1].StoryFlag; set => _specials[1].StoryFlag = value; }

    public byte Special3Offset { get => _specials[2].MoveOffset; set => _specials[2].MoveOffset = value; }
    public byte Special3StartingLevel { get => _specials[2].StartingLevel; set => _specials[2].StartingLevel = value; }
    public ushort Special3StoryFlag { get => _specials[2].StoryFlag; set => _specials[2].StoryFlag = value; }

    public byte Special4Offset { get => _specials[3].MoveOffset; set => _specials[3].MoveOffset = value; }
    public byte Special4StartingLevel { get => _specials[3].StartingLevel; set => _specials[3].StartingLevel = value; }
    public ushort Special4StoryFlag { get => _specials[3].StoryFlag; set => _specials[3].StoryFlag = value; }

    public byte Special5Offset { get => _specials[4].MoveOffset; set => _specials[4].MoveOffset = value; }
    public byte Special5StartingLevel { get => _specials[4].StartingLevel; set => _specials[4].StartingLevel = value; }
    public ushort Special5StoryFlag { get => _specials[4].StoryFlag; set => _specials[4].StoryFlag = value; }

    public byte Special6Offset { get => _specials[5].MoveOffset; set => _specials[5].MoveOffset = value; }
    public byte Special6StartingLevel { get => _specials[5].StartingLevel; set => _specials[5].StartingLevel = value; }
    public ushort Special6StoryFlag { get => _specials[5].StoryFlag; set => _specials[5].StoryFlag = value; }
}