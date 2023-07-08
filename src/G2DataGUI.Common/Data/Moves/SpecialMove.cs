using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class SpecialMove
{
    public byte MoveOffset { get; set; }
    public byte StartingLevel { get; set; }
    public ushort StoryFlag { get; set; }

	public static string CSVHeader =>
		$"Special,Starting Level,Story Flag";

    public static SpecialMove ReadSpecialMove(Stream reader)
    {
        SpecialMove specialMove = new()
        {
            MoveOffset = reader.ReadRawByte(),
            StartingLevel = reader.ReadRawByte(),
            StoryFlag = reader.ReadRawUShort()
        };

        return specialMove;
    }

    public void WriteSpecialMove(Stream writer)
    {
        writer.WriteRawByte(MoveOffset);
        writer.WriteRawByte(StartingLevel);
        writer.WriteRawUShort(StoryFlag);
    }

    public void GenerageCSV(StreamWriter writer)
    {
        writer.Write(
			$"{Moves.Instance.GameMoves[MoveOffset].Name}," +
			$"{StartingLevel}," +
			$"{StoryFlag}");
    }
}
