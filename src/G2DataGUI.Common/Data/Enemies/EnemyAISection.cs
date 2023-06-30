using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyAISection
{
    public EnemyAI[] AIs = new EnemyAI[5];

    public static EnemyAISection ReadEnemyAISection(Stream reader)
    {
        EnemyAISection enemyAISection = new EnemyAISection();
        for (int index = 0; index < enemyAISection.AIs.Length; index++)
        {
            EnemyAI? ai = reader.ReadStruct<EnemyAI>();
            if (ai == null) return null;
            enemyAISection.AIs[index] = ai.Value;
        }

        return enemyAISection;
    }
}