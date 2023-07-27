using G2DataGUI.Common.Data.Common;
using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class Manaegg : BaseContainer
{
    public ManaeggSpell Spell1 { get; set; }
    public ManaeggSpell Spell2 { get; set; }
    public ManaeggSpell Spell3 { get; set; }
    public ManaeggSpell Spell4 { get; set; }
    public ManaeggSpell Spell5 { get; set; }
    public ManaeggSpell Spell6 { get; set; }
    public ManaeggSpell Spell7 { get; set; }
    public ManaeggSpell Spell8 { get; set; }
    public ManaeggSpell Spell9 { get; set; }
    public ManaeggSpell Spell10 { get; set; }
    public ManaeggSpell Spell11 { get; set; }
    public ManaeggSpell Spell12 { get; set; }
    public ManaeggSpell Spell13 { get; set; }
    public ManaeggSpell Spell14 { get; set; }
    public ManaeggSpell Spell15 { get; set; }
    public ManaeggSpell Spell16 { get; set; }
    public ManaeggSpell Spell17 { get; set; }
    public ManaeggSpell Spell18 { get; set; }

    public static string CSVHeader =>
        $"{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader}," +
        $"{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader}," +
        $"{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader}," +
        $"{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader}," +
        $"{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader}," +
        $"{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader},{ManaeggSpell.CSVHeader}";

    public static Manaegg ReadManaegg(Stream reader)
    {
		Manaegg manaegg = new()
		{
			Spell1 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell2 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell3 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell4 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell5 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell6 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell7 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell8 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell9 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell10 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell11 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell12 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell13 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell14 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell15 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell16 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell17 = ManaeggSpell.ReadManaeggSpell(reader),
			Spell18 = ManaeggSpell.ReadManaeggSpell(reader)
		};

		return manaegg;
    }

    public void WriteManaegg(Stream writer)
    {
        Spell1.WriteManaeggSpell(writer);
        Spell2.WriteManaeggSpell(writer);
        Spell3.WriteManaeggSpell(writer);
        Spell4.WriteManaeggSpell(writer);
        Spell5.WriteManaeggSpell(writer);
        Spell6.WriteManaeggSpell(writer);
        Spell7.WriteManaeggSpell(writer);
        Spell8.WriteManaeggSpell(writer);
        Spell9.WriteManaeggSpell(writer);
        Spell10.WriteManaeggSpell(writer);
        Spell11.WriteManaeggSpell(writer);
        Spell12.WriteManaeggSpell(writer);
        Spell13.WriteManaeggSpell(writer);
        Spell14.WriteManaeggSpell(writer);
        Spell15.WriteManaeggSpell(writer);
        Spell16.WriteManaeggSpell(writer);
        Spell17.WriteManaeggSpell(writer);
        Spell18.WriteManaeggSpell(writer);
    }

    public void GenerateCSV(StreamWriter writer)
    {
        Spell1.GenerateCSV(writer);
		writer.Write(',');
        Spell2.GenerateCSV(writer);
		writer.Write(',');
        Spell3.GenerateCSV(writer);
		writer.Write(",");
        Spell4.GenerateCSV(writer);
		writer.Write(",");
        Spell5.GenerateCSV(writer);
		writer.Write(",");
        Spell6.GenerateCSV(writer);
		writer.Write(',');
        Spell7.GenerateCSV(writer);
		writer.Write(',');
        Spell8.GenerateCSV(writer);
		writer.Write(',');
        Spell9.GenerateCSV(writer);
		writer.Write(',');
        Spell10.GenerateCSV(writer);
		writer.Write(',');
        Spell11.GenerateCSV(writer);
		writer.Write(',');
        Spell12.GenerateCSV(writer);
		writer.Write(',');
        Spell13.GenerateCSV(writer);
		writer.Write(',');
        Spell14.GenerateCSV(writer);
		writer.Write(',');
        Spell15.GenerateCSV(writer);
		writer.Write(',');
        Spell16.GenerateCSV(writer);
		writer.Write(',');
        Spell17.GenerateCSV(writer);
		writer.Write(',');
        Spell18.GenerateCSV(writer);
		writer.WriteLine();
    }
}
