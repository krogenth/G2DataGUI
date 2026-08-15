using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Domain.Skills;

public sealed class Skill : IIdentifiable<int>
{
	/// <summary>Position within SK_PARAM.BIN.</summary>
	public int Id { get; set; }

	public string Name { get; set; } = string.Empty;
	public SkillStats Stats { get; set; } = new();
	public string Description { get; set; } = string.Empty;

	public const int MaxNameLength = 18;
	public const int MaxDescriptionLength = 40;
}
