using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;
public class BossStatsViewModel : BaseViewModel
{
	public EnemyStats SelectedBossStats { get; set; }
	public ObservableCollection<Item> GameItems { get; set; } = Items.Instance.GameItems;

	public BossStatsViewModel(EnemyStats stats)
	{
		SelectedBossStats = stats;
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
	}

	private void ItemsCollectionRefreshed(object? sender, EventArgs e) =>
		OnPropertyChanged(nameof(GameItems));
}
