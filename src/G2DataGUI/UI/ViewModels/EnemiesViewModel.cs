using G2DataGUI.Common.Data.Enemies;
using System;
using System.Collections.ObjectModel;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class EnemiesViewModel : BaseViewModel
{
    public ObservableCollection<Enemy> GameEnemies { get; private set; } = Enemies.Instance.GameEnemies;
    private int _selectedEnemyIndex;
    private Enemy _selectedEnemyItem;
	private readonly EnemyStatsViewModel _enemyStatsViewModel = EnemyStatsViewModel.Instance;
    private readonly EnemyAISectionViewModel _enemyAISectionViewModel = EnemyAISectionViewModel.Instance;
    private readonly EnemyMovesetViewModel _enemyMovesetViewModel = EnemyMovesetViewModel.Instance;

    public static EnemiesViewModel Instance { get; private set; } = new();

    private EnemiesViewModel()
    {
		SelectedEnemyIndex = 0;
        Enemies.Instance.CollectionRefreshed += EnemiesCollectionRefreshed;
    }

	private void EnemiesCollectionRefreshed(object sender, EventArgs _) =>
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

			if (value >= GameEnemies.Count)
			{
				return;
			}

			_selectedEnemyIndex = value;
            SelectedEnemyItem = GameEnemies[value];
            OnPropertyChanged(nameof(SelectedEnemyIndex));
        }
    }

    public Enemy SelectedEnemyItem
    {
        get => _selectedEnemyItem;
        set
        {
            _selectedEnemyItem = value;
			_enemyStatsViewModel.SelectedEnemyStats = value.Stats;
			_enemyAISectionViewModel.SelectedEnemyAISection = value.AISection;
            _enemyMovesetViewModel.SelectedEnemyMoves = value.Moveset.Moves;
            OnPropertyChanged(nameof(SelectedEnemyItem));
        }
    }
}
