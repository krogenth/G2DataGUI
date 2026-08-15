using G2DataGUI.Domain.Skills;
using G2DataGUI.Repositories.Skills;

namespace G2DataGUI.Export.Skills;

public sealed class SkillbookSkillCsvExporter(SkillRepository skills)
{
	public string Header => "Skill,Starting Level,Required Level,Unknown #1";

	public void WriteFields(TextWriter writer, SkillbookSkill skill)
	{
		string skillName = skills.Find(skill.SkillOffset)?.Name ?? $"<unknown {skill.SkillOffset}>";

		writer.Write($"{skillName},{skill.StartingLevel},{skill.RequiredLevel},{skill.Unknown1}");
	}
}
