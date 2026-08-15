using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Domain.Skills;

public sealed class Skillbook : IIdentifiable<int>
{
	public const int SkillCount = 6;

	/// <summary>Position within TB_SKILL.BIN.</summary>
	public int Id { get; set; }

	public List<SkillbookSkill> Skills { get; set; } = [];
}
