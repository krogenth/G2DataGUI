using G2DataGUI.Domain.Skills;
using G2DataGUI.Repositories.Skills;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Skills;

public sealed class SkillsViewModel(SkillRepository repository)
	: CollectionViewModel<Skill, SkillViewModel>(repository.Items, model => new SkillViewModel(model, repository));
