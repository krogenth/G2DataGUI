using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.Common.Data.Items;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace G2DataGUI.UI.ViewModels;

public class EnemiesViewModel : BaseViewModel
{
    public ObservableCollection<Enemy> GameEnemies { get; private set; } = Enemies.Instance.GetEnemies();
    public ObservableCollection<Item> GameItems { get; private set; } = Items.Instance.GetItems();
    private int _selectedEnemyIndex = 0;
    private Enemy _selectedEnemyItem;
    private EnemyAISectionViewModel _enemyAISectionViewModel = EnemyAISectionViewModel.Instance;
    private EnemyMovesetViewModel _enemyMovesetViewModel = EnemyMovesetViewModel.Instance;

    public static EnemiesViewModel Instance { get; private set; } = new();

    private EnemiesViewModel()
    {
        SelectedEnemyItem = GameEnemies.First();
        _enemyAISectionViewModel.SelectedEnemyAISection = GameEnemies[SelectedEnemyIndex].AISection;
        _enemyMovesetViewModel.SelectedEnemyMoveset = GameEnemies[SelectedEnemyIndex].Moveset;
        Enemies.Instance.CollectionRefreshed += EnemiesCollectionRefreshed;
    }

    private void EnemiesCollectionRefreshed(object sender, EventArgs _)
    {
        SelectedEnemyItem = GameEnemies[SelectedEnemyIndex];
        _enemyAISectionViewModel.SelectedEnemyAISection = GameEnemies[SelectedEnemyIndex].AISection;
        _enemyMovesetViewModel.SelectedEnemyMoveset = GameEnemies[SelectedEnemyIndex].Moveset;
    }

    public int SelectedEnemyIndex
    {
        get => _selectedEnemyIndex;
        set
        {
            if (value < 0) value = 0;
            if (value >= GameEnemies.Count) return;
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
            _enemyAISectionViewModel.SelectedEnemyAISection = value.AISection;
            _enemyMovesetViewModel.SelectedEnemyMoveset = value.Moveset;
            OnPropertyChanged(nameof(SelectedEnemyItem));
        }
    }
}