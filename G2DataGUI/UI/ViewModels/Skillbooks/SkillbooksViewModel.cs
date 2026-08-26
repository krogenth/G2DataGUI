using System.Collections.ObjectModel;
using G2DataGUI.Domain.Skills;
using G2DataGUI.Repositories.Skills;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Skills;

namespace G2DataGUI.UI.ViewModels.Skillbooks;

public sealed class SkillbooksViewModel(SkillbookRepository repository, ObservableCollection<SkillViewModel> availableSkills)
	: CollectionViewModel<Skillbook, SkillbookViewModel>(repository.Items, model => new SkillbookViewModel(model, repository, availableSkills));
