using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyMoveset
{
	public const int NumberOfMoves = 5;
	public EnemyMove[] Moves { get; set; } = new EnemyMove[NumberOfMoves];

	public static string CSVHeader =>
		$"{EnemyMove.CSVHeader}";

    public static EnemyMoveset ReadEnemyMoveset(Stream reader)
    {
		EnemyMoveset moveset = new()
		{
			Moves = new EnemyMove[NumberOfMoves]
			{
				EnemyMove.ReadEnemyMove(reader),
				EnemyMove.ReadEnemyMove(reader),
				EnemyMove.ReadEnemyMove(reader),
				EnemyMove.ReadEnemyMove(reader),
				EnemyMove.ReadEnemyMove(reader),
			},
		};

		return moveset;
    }

    public void WriteEnemyMoveset(Stream writer)
    {
        foreach (var move in Moves)
		{
			move.WriteEnemyMove(writer);
		}
    }

	public void GenerateCSV(StreamWriter writer)
	{
		foreach (var move in Moves)
		{
			move.GenerateCSV(writer);
			writer.WriteLine();
		}
	}
}
