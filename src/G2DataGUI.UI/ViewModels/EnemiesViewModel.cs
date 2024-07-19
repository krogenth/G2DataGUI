using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;
using System.Collections.ObjectModel;

namespace G2DataGUI.UI.ViewModels;

public class EnemiesViewModel : BaseViewModel
{
	public ObservableCollection<Enemy> GameEnemies { get; set; } = Enemies.Instance.GameEnemies;
	private int _selectedEnemyIndex = 0;
	public Enemy? SelectedEnemyItem { get; set; }
	public EnemyStatsViewModel SelectedEnemyStatsViewModel { get; set; } = new(new());
	public EnemyAISectionViewModel SelectedEnemyAISectionViewModel { get; set; } = new(new());
	public EnemyMovesetViewModel SelectedEnemyMovesetViewModel { get; set; } = new(new());

	public EnemiesViewModel()
	{
		SelectedEnemyIndex = 0;
		Enemies.Instance.CollectionRefreshed += EnemiesCollectionRefreshed;
	}

	private void EnemiesCollectionRefreshed(object? sender, EventArgs _) =>
		SelectedEnemyIndex = SelectedEnemyIndex;

	public int SelectedEnemyIndex
	{
		get => _selectedEnemyIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			else if (value >= GameEnemies.Count)
			{
				value = GameEnemies.Count - 1;
			}

			if (GameEnemies.Count == 0)
			{
				return;
			}

			_selectedEnemyIndex = value;
			SelectedEnemyItem = GameEnemies[value];
			SelectedEnemyStatsViewModel = new(SelectedEnemyItem.Stats);
			SelectedEnemyAISectionViewModel = new(SelectedEnemyItem.AISection);
			SelectedEnemyMovesetViewModel = new(SelectedEnemyItem.Moveset);
			OnPropertyChanged(nameof(SelectedEnemyIndex));
			OnPropertyChanged(nameof(SelectedEnemyItem));
			OnPropertyChanged(nameof(SelectedEnemyStatsViewModel));
			OnPropertyChanged(nameof(SelectedEnemyAISectionViewModel));
			OnPropertyChanged(nameof(SelectedEnemyMovesetViewModel));
		}
	}
}
