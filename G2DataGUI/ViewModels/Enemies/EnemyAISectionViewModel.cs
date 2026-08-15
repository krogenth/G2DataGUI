using System.Collections.Generic;
using System.Linq;
using G2DataGUI.Domain.Enemies;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Enemies;

/// <summary>
/// Conditions is fixed-size (EnemyAISection.ConditionsPerSection =
/// 5) — projected once at construction, same convention as
/// ManaeggViewModel.Spells.
/// </summary>
public sealed class EnemyAISectionViewModel(EnemyAISection model, IRepository repository) : ItemViewModel<EnemyAISection>(model, repository)
{
	public IReadOnlyList<EnemyAIViewModel> Conditions { get; } = model.Conditions.Select(condition => new EnemyAIViewModel(condition, repository)).ToList();
}
