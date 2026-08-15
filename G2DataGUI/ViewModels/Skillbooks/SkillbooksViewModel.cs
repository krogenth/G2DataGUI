using G2DataGUI.Domain.Skills;
using G2DataGUI.Repositories.Skills;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Skillbooks;

public sealed class SkillbooksViewModel(SkillbookRepository repository)
	: CollectionViewModel<Skillbook, SkillbookViewModel>(repository.Items, model => new SkillbookViewModel(model, repository));
