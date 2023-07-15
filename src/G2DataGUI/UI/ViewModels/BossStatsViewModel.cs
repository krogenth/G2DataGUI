using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;
using System;

namespace G2DataGUI.UI.ViewModels;
public class BossStatsViewModel : BaseViewModel
{
	private EnemyStats _selectedBossStats = new();
	public ObservableCollection<Item> GameItems { get; private set; } = Items.Instance.GameItems;

	public static BossStatsViewModel Instance { get; private set; } = new();

	private BossStatsViewModel()
	{
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
	}

	private void ItemsCollectionRefreshed(object sender, EventArgs e) =>
		OnPropertyChanged(nameof(GameItems));

	public EnemyStats SelectedBossStats
	{
		get => _selectedBossStats;
		set
		{
			_selectedBossStats = value == null ? new EnemyStats() : value;
			OnPropertyChanged(nameof(SelectedBossStats));
		}
	}
}
