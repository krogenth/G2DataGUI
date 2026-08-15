using G2DataGUI.Domain.Skills;
using G2DataGUI.Serialization.Skills;

namespace G2DataGUI.Repositories.Skills;

public sealed class SkillbookRepository(string filePath)
	: Repository<Skillbook, int>("Skillbooks", filePath, new SkillbookReader(), new SkillbookWriter());
