using G2DataGUI.Domain.Skills;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Skillbooks;

public sealed class SkillbookSkillViewModel(SkillbookSkill model, IRepository repository)
	: ItemViewModel<SkillbookSkill>(model, repository)
{
	public byte SkillOffset
	{
		get => Model.SkillOffset;
		set => SetModelValue(value, m => m.SkillOffset, (m, v) => m.SkillOffset = v);
	}

	public byte StartingLevel
	{
		get => Model.StartingLevel;
		set => SetModelValue(value, m => m.StartingLevel, (m, v) => m.StartingLevel = v);
	}

	public byte RequiredLevel
	{
		get => Model.RequiredLevel;
		set => SetModelValue(value, m => m.RequiredLevel, (m, v) => m.RequiredLevel = v);
	}

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}
}
