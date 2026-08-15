namespace G2DataGUI.Domain.Skills;

public sealed class SkillbookSkill
{
	/// <summary>Index into the Skills table. Resolved to a name at export
	/// time via a SkillRepository lookup — see SkillbookSkillCsvExporter.</summary>
	public byte SkillOffset { get; set; }
	public byte StartingLevel { get; set; }
	public byte RequiredLevel { get; set; }
	public byte Unknown1 { get; set; }
}
