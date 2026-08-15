using System.Collections.Generic;
using System.Linq;
using G2DataGUI.Domain.Enemies;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Enemies;

/// <summary>
/// Moves is fixed-size (EnemyMoveset.MovesPerSet = 5) — projected
/// once at construction, same convention as EnemyAISectionViewModel.Conditions.
/// </summary>
public sealed class EnemyMovesetViewModel(EnemyMoveset model, IRepository repository) : ItemViewModel<EnemyMoveset>(model, repository)
{
	public IReadOnlyList<EnemyMoveViewModel> Moves { get; } = model.Moves.Select(move => new EnemyMoveViewModel(move, repository)).ToList();
}
