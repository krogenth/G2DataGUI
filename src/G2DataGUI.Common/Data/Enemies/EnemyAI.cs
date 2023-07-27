using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyAI
{
    public byte Condition { get; set; }
    public byte Move1Chance { get; set; }
    public byte Move2Chance { get; set; }
    public byte Move3Chance { get; set; }
    public byte Move4Chance { get; set; }
    public byte Move5Chance { get; set; }

	public static string CSVHeader =>
		$"Condition,Move #1 Chance,Move #2 Chance,Move #3 Chance,Move #4 Chance,Move #5 Chance";

    public static EnemyAI ReadEnemyAI(Stream reader)
    {
		EnemyAI ai = new()
		{
			Condition = reader.ReadRawByte(),
			Move1Chance = reader.ReadRawByte(),
			Move2Chance = reader.ReadRawByte(),
			Move3Chance = reader.ReadRawByte(),
			Move4Chance = reader.ReadRawByte(),
			Move5Chance = reader.ReadRawByte()
		};

		return ai;
    }

    public void WriteEnemyAI(Stream writer)
    {
        writer.WriteRawByte(Condition);
        writer.WriteRawByte(Move1Chance);
        writer.WriteRawByte(Move2Chance);
        writer.WriteRawByte(Move3Chance);
        writer.WriteRawByte(Move4Chance);
        writer.WriteRawByte(Move5Chance);
    }

	public void GenerateCSV(StreamWriter writer)
	{
		writer.Write(
			$"{Condition}," +
			$"{Move1Chance}," +
			$"{Move2Chance}," +
			$"{Move3Chance}," +
			$"{Move4Chance}," +
			$"{Move5Chance}");
	}
}
