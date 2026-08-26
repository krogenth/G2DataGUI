using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Domain.Skills;
using G2DataGUI.Repositories.Skills;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Skills;

namespace G2DataGUI.UI.ViewModels.Skillbooks;

/// <summary>Skills is fixed-size (Skillbook.SkillCount = 6) — projected
/// once at construction, same convention as ManaeggViewModel.Spells. Only
/// one page (unlike Manaeggs' 3) — 6 is already exactly one page's worth,
/// so there's no Pages/chunking here at all.</summary>
public sealed class SkillbookViewModel(Skillbook model, SkillbookRepository repository, ObservableCollection<SkillViewModel> availableSkills)
	: ItemViewModel<Skillbook>(model, repository)
{
	public int Id => Model.Id;

	public IReadOnlyList<SkillbookSkillViewModel> Skills { get; } =
		model.Skills.Select(skill => new SkillbookSkillViewModel(skill, repository, availableSkills)).ToList();
}
