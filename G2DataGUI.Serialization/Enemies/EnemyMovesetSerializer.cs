using G2DataGUI.Domain.Enemies;

namespace G2DataGUI.Serialization.Enemies;

public static class EnemyMovesetSerializer
{
	public static EnemyMoveset Read(Stream stream)
	{
		List<EnemyMove> moves = [];

		for (int i = 0; i < EnemyMoveset.MovesPerSet; i++)
		{
			moves.Add(EnemyMoveSerializer.Read(stream));
		}

		return new EnemyMoveset { Moves = moves };
	}

	public static void Write(Stream stream, EnemyMoveset moveset)
	{
		foreach (EnemyMove move in moveset.Moves)
		{
			EnemyMoveSerializer.Write(stream, move);
		}
	}
}
