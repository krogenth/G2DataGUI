using System;
using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapShopViewModel : BaseViewModel
{
	public ObservableCollection<Item> GameItems { get; private set; } = Items.Instance.GameItems;
	private MapShop _selectedMapShop = new();

	public static MapShopViewModel Instance { get; private set; } = new();

	private MapShopViewModel()
	{
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
	}

	private void ItemsCollectionRefreshed(object sender, EventArgs e) =>
		OnPropertyChanged(nameof(GameItems));

	public MapShop SelectedMapShop
	{
		get => _selectedMapShop;
		set
		{
			_selectedMapShop = value;
			OnPropertyChanged(nameof(SelectedMapShop));
			OnPropertyChanged(nameof(HasMapShop));
		}
	}

	public bool HasMapShop => _selectedMapShop != null;
}
