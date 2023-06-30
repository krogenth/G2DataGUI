using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class SpecialSet
{
    public SpecialMove Special1 { get; set; }
    public SpecialMove Special2 { get; set; }
    public SpecialMove Special3 { get; set; }
    public SpecialMove Special4 { get; set; }
    public SpecialMove Special5 { get; set; }
    public SpecialMove Special6 { get; set; }

    public static SpecialSet ReadSpecialSet(Stream reader)
    {
        SpecialSet specialSet = new SpecialSet();
        specialSet.Special1 = SpecialMove.ReadSpecialMove(reader);
        specialSet.Special2 = SpecialMove.ReadSpecialMove(reader);
        specialSet.Special3 = SpecialMove.ReadSpecialMove(reader);
        specialSet.Special4 = SpecialMove.ReadSpecialMove(reader);
        specialSet.Special5 = SpecialMove.ReadSpecialMove(reader);
        specialSet.Special6 = SpecialMove.ReadSpecialMove(reader);

        return specialSet;
    }

    public void WriteSpecialSet(Stream writer)
    {
        Special1.WriteSpecialMove(writer);
        Special2.WriteSpecialMove(writer);
        Special3.WriteSpecialMove(writer);
        Special4.WriteSpecialMove(writer);
        Special5.WriteSpecialMove(writer);
        Special6.WriteSpecialMove(writer);
    }
}