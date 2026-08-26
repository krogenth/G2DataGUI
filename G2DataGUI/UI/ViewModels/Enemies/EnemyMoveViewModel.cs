using G2DataGUI.Domain.Enemies;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Enemies;

public sealed class EnemyMoveViewModel : ItemViewModel<EnemyMove>
{
	public EnemyMoveViewModel(EnemyMove model, IRepository repository) : base(model, repository)
	{
		Stats = new EnemyMoveStatsViewModel(model.Stats, repository);
	}

	public string Name
	{
		get => Model.Name;
		set => SetModelValue(value, m => m.Name, (m, v) => m.Name = v);
	}

	public EnemyMoveStatsViewModel Stats { get; }
}
