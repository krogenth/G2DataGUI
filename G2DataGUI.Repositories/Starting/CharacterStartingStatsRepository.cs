using G2DataGUI.Domain.Starting;
using G2DataGUI.Serialization.Starting;

namespace G2DataGUI.Repositories.Starting;

public sealed class CharacterStartingStatsRepository(string filePath)
	: Repository<CharacterStartingStats, int>("Initial", filePath, new CharacterStartingStatsReader(), new CharacterStartingStatsWriter());
