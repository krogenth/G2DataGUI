using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapShopViewModel : BaseViewModel
{
	public ObservableCollection<Item> GameItems { get; set; } = Items.Instance.GameItems;
	public MapShop SelectedMapShop { get; set; } = new();

    public MapShopViewModel(MapShop shop)
	{
		SelectedMapShop = shop;
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
	}

	private void ItemsCollectionRefreshed(object? sender, EventArgs e) =>
		OnPropertyChanged(nameof(GameItems));
}
