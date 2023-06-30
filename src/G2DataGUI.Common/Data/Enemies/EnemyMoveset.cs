using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyMoveset
{
    public EnemyMove[] Moves = new EnemyMove[5];

    public static EnemyMoveset ReadEnemyMoveset(Stream reader)
    {
        EnemyMoveset enemyMoveset = new EnemyMoveset();
        for (int index = 0; index < enemyMoveset.Moves.Length; index++)
        {
            EnemyMove? move = reader.ReadStruct<EnemyMove>();
            if (move == null) return null;
            enemyMoveset.Moves[index] = move.Value;
        }

        return enemyMoveset;
    }
}