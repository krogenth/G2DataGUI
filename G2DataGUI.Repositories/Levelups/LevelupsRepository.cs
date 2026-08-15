using G2DataGUI.Domain.Levelups;
using G2DataGUI.Serialization.Levelups;

namespace G2DataGUI.Repositories.Levelups;

public sealed class LevelupsRepository(string filePath)
	: Repository<CharacterLevelups, int>("Levelups", filePath, new CharacterLevelupsReader(), new CharacterLevelupsWriter());
