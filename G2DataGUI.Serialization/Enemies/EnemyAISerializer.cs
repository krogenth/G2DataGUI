using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Enemies;

namespace G2DataGUI.Serialization.Enemies;

public static class EnemyAISerializer
{
	public static EnemyAI Read(Stream stream) => new()
	{
		Condition = stream.ReadRawByte(),
		Move1Chance = stream.ReadRawByte(),
		Move2Chance = stream.ReadRawByte(),
		Move3Chance = stream.ReadRawByte(),
		Move4Chance = stream.ReadRawByte(),
		Move5Chance = stream.ReadRawByte()
	};

	public static void Write(Stream stream, EnemyAI ai)
	{
		stream.WriteRawByte(ai.Condition);
		stream.WriteRawByte(ai.Move1Chance);
		stream.WriteRawByte(ai.Move2Chance);
		stream.WriteRawByte(ai.Move3Chance);
		stream.WriteRawByte(ai.Move4Chance);
		stream.WriteRawByte(ai.Move5Chance);
	}
}
