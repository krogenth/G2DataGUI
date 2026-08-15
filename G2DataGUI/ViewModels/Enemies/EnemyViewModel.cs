using G2DataGUI.Domain.Enemies;
using G2DataGUI.Repositories.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Enemies;

/// <summary>
/// No Id setter, and no FilePath/IsSecond exposed for editing —
/// both are how EnemyRepository finds this entry's *_0.dat and which
/// pointer offset to write back to (see EnemyEntrySerializer), not user
/// data.
/// </summary>
public sealed class EnemyViewModel : ItemViewModel<Enemy>
{
	public EnemyViewModel(Enemy model, EnemyRepository repository) : base(model, repository)
	{
		Stats = new EnemyStatsViewModel(model.Stats, repository);
		AISection = new EnemyAISectionViewModel(model.AISection, repository);
		Moveset = new EnemyMovesetViewModel(model.Moveset, repository);
	}

	public int Id => Model.Id;

	public EnemyStatsViewModel Stats { get; }

	public EnemyAISectionViewModel AISection { get; }

	public EnemyMovesetViewModel Moveset { get; }
}
