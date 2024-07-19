using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;
public class EnemyStatsViewModel : BaseViewModel
{
	public EnemyStats SelectedEnemyStats { get; set; } = new();
	public ObservableCollection<Item> GameItems { get; set; } = Items.Instance.GameItems;

	public EnemyStatsViewModel(EnemyStats stats)
	{
		SelectedEnemyStats = stats;
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
	}

	private void ItemsCollectionRefreshed(object? sender, EventArgs e) =>
		OnPropertyChanged(nameof(GameItems));
}
