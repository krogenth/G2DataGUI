using System;
using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;
public class EnemyStatsViewModel : BaseViewModel
{
	private EnemyStats _selectedEnemyStats = new();
	public ObservableCollection<Item> GameItems { get; private set; } = Items.Instance.GameItems;

	public static EnemyStatsViewModel Instance { get; private set; } = new();

	private EnemyStatsViewModel()
	{
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
	}

	private void ItemsCollectionRefreshed(object sender, EventArgs e) =>
		OnPropertyChanged(nameof(GameItems));

	public EnemyStats SelectedEnemyStats
	{
		get => _selectedEnemyStats;
		set
		{
			_selectedEnemyStats = value == null  ? new EnemyStats() : value;
			OnPropertyChanged(nameof(SelectedEnemyStats));
		}
	}
}
