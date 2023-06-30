using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class SpecialMove
{
    public byte MoveOffset { get; set; }
    public byte StartingLevel { get; set; }
    public ushort StoryFlag { get; set; }

    public static SpecialMove ReadSpecialMove(Stream reader)
    {
        SpecialMove specialMove = new SpecialMove();
        specialMove.MoveOffset = reader.ReadRawByte();
        specialMove.StartingLevel = reader.ReadRawByte();
        specialMove.StoryFlag = reader.ReadRawUShort();

        return specialMove;
    }

    public void WriteSpecialMove(Stream writer)
    {
        writer.WriteRawByte(MoveOffset);
        writer.WriteRawByte(StartingLevel);
        writer.WriteRawUShort(StoryFlag);
    }
}