using G2DataGUI.Domain.Enemies;
using G2DataGUI.Serialization.Common;

namespace G2DataGUI.Serialization.Enemies;

public static class EnemyMoveSerializer
{
	private const int NameLength = 18;

	public static EnemyMove Read(Stream stream) => new()
	{
		Name = FixedString.ReadLeftPadded(stream, NameLength),
		Stats = EnemyMoveStatsSerializer.Read(stream)
	};

	public static void Write(Stream stream, EnemyMove move)
	{
		FixedString.WriteLeftPadded(stream, move.Name, NameLength);
		EnemyMoveStatsSerializer.Write(stream, move.Stats);
	}
}
