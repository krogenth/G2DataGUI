using G2DataGUI.Domain.Skills;
using G2DataGUI.Serialization.Skills;

namespace G2DataGUI.Repositories.Skills;

public sealed class SkillRepository(string filePath)
	: Repository<Skill, int>("Skills", filePath, new SkillReader(), new SkillWriter());
