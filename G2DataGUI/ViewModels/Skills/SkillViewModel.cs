using G2DataGUI.Domain.Skills;
using G2DataGUI.Repositories.Skills;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Skills;

public sealed class SkillViewModel : ItemViewModel<Skill>
{
	public SkillViewModel(Skill model, SkillRepository repository) : base(model, repository)
	{
		Stats = new SkillStatsViewModel(model.Stats, repository);
	}

	public int Id => Model.Id;

	public string Name
	{
		get => Model.Name;
		set => SetModelValue(value, m => m.Name, (m, v) => m.Name = v);
	}

	public SkillStatsViewModel Stats { get; }

	public string Description
	{
		get => Model.Description;
		set => SetModelValue(value, m => m.Description, (m, v) => m.Description = v);
	}
}
