using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyMoveset
{
    public EnemyMove Move1 { get; set; }
    public EnemyMove Move2 { get; set; }
    public EnemyMove Move3 { get; set; }
    public EnemyMove Move4 { get; set; }
    public EnemyMove Move5 { get; set; }

    public static EnemyMoveset ReadEnemyMoveset(Stream reader)
    {
		EnemyMoveset moveset = new()
		{
			Move1 = EnemyMove.ReadEnemyMove(reader),
			Move2 = EnemyMove.ReadEnemyMove(reader),
			Move3 = EnemyMove.ReadEnemyMove(reader),
			Move4 = EnemyMove.ReadEnemyMove(reader),
			Move5 = EnemyMove.ReadEnemyMove(reader)
		};

		return moveset;
    }

    public void WriteEnemyMoveset(Stream writer)
    {
        Move1.WriteEnemyMove(writer);
        Move2.WriteEnemyMove(writer);
        Move3.WriteEnemyMove(writer);
        Move4.WriteEnemyMove(writer);
        Move5.WriteEnemyMove(writer);
    }
}