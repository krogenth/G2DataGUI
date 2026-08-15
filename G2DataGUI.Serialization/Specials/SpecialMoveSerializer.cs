using G2DataGUI.Domain.Specials;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Specials;

public static class SpecialMoveSerializer
{
	public static SpecialMove Read(Stream stream) => new()
	{
		MoveOffset = stream.ReadRawByte(),
		StartingLevel = stream.ReadRawByte(),
		StoryFlag = stream.ReadRawUShort()
	};

	public static void Write(Stream stream, SpecialMove move)
	{
		stream.WriteRawByte(move.MoveOffset);
		stream.WriteRawByte(move.StartingLevel);
		stream.WriteRawUShort(move.StoryFlag);
	}
}
