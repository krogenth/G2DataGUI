using System.Collections.Generic;
using System.Linq;
using G2DataGUI.Domain.Skills;
using G2DataGUI.Repositories.Skills;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Skillbooks;

/// <summary>Skills is fixed-size (Skillbook.SkillCount = 6) — projected
/// once at construction, same convention as ManaeggViewModel.Spells.</summary>
public sealed class SkillbookViewModel(Skillbook model, SkillbookRepository repository) : ItemViewModel<Skillbook>(model, repository)
{
	public int Id => Model.Id;

	public IReadOnlyList<SkillbookSkillViewModel> Skills { get; } = model.Skills.Select(skill => new SkillbookSkillViewModel(skill, repository)).ToList();
}
