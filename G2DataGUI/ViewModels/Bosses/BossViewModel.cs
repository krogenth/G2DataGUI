using System.Collections.Generic;
using System.Linq;
using G2DataGUI.Domain.Bosses;
using G2DataGUI.Repositories.Bosses;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.ViewModels.Enemies;

namespace G2DataGUI.ViewModels.Bosses;

/// <summary>
/// Boss reuses EnemyStatsViewModel/EnemyAISectionViewModel/
/// EnemyMovesetViewModel — Boss.Stats/AISection are the identical binary
/// layout as Enemy's (see EnemyStats' doc comment), and each entry in
/// Movesets is the same EnemyMoveset shape too. Movesets is currently
/// always a single entry — see Boss.Movesets' doc comment on why (an
/// unread zXX_act.dat in the original) — projected here the same way as
/// other fixed lists rather than using CollectionViewModel, since the
/// whole BossViewModel gets rebuilt on reload anyway.
/// </summary>
public sealed class BossViewModel(Boss model, BossRepository repository) : ItemViewModel<Boss>(model, repository)
{
	public int Id => Model.Id;

	public EnemyStatsViewModel Stats { get; } = new EnemyStatsViewModel(model.Stats, repository);

	public EnemyAISectionViewModel AISection { get; } = new EnemyAISectionViewModel(model.AISection, repository);

	public IReadOnlyList<EnemyMovesetViewModel> Movesets { get; } = model.Movesets.Select(moveset => new EnemyMovesetViewModel(moveset, repository)).ToList();
}
