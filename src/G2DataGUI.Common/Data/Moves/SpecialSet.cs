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

	public static string CSVHeader =>
		$"{SpecialMove.CSVHeader},{SpecialMove.CSVHeader},{SpecialMove.CSVHeader}," +
		$"{SpecialMove.CSVHeader},{SpecialMove.CSVHeader},{SpecialMove.CSVHeader}";

    public static SpecialSet ReadSpecialSet(Stream reader)
    {
        SpecialSet specialSet = new()
        {
            Special1 = SpecialMove.ReadSpecialMove(reader),
            Special2 = SpecialMove.ReadSpecialMove(reader),
            Special3 = SpecialMove.ReadSpecialMove(reader),
            Special4 = SpecialMove.ReadSpecialMove(reader),
            Special5 = SpecialMove.ReadSpecialMove(reader),
            Special6 = SpecialMove.ReadSpecialMove(reader),
        };

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

    public void GenerateCSV(StreamWriter writer)
    {
        Special1.GenerageCSV(writer);
        writer.Write(',');
        Special2.GenerageCSV(writer);
        writer.Write(',');
        Special3.GenerageCSV(writer);
        writer.Write(',');
        Special4.GenerageCSV(writer);
        writer.Write(',');
        Special5.GenerageCSV(writer);
        writer.Write(',');
        Special6.GenerageCSV(writer);
        writer.WriteLine();
    }
}
