using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapIconsViewModel : BaseViewModel
{
	public ObservableCollection<Item> GameItems { get; set; } = Items.Instance.GameItems;
	public List<MapIcon> SelectedMapIcons { get; set; } = [];
	private int _selectedMapIconIndex = 0;
	public MapIcon SelectedMapIconItem { get; set; } = new();

	public MapIconsViewModel(List<MapIcon> icons)
	{
		SelectedMapIcons = icons;
		SelectedMapIconIndex = 0;
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
	}

	private void ItemsCollectionRefreshed(object? sender, EventArgs e) =>
		OnPropertyChanged(nameof(GameItems));

	public int SelectedMapIconIndex
	{
		get => _selectedMapIconIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (SelectedMapIcons.Count == 0)
			{
				return;
			}

			_selectedMapIconIndex = value;
			SelectedMapIconItem = SelectedMapIcons[value];
			OnPropertyChanged(nameof(SelectedMapIconIndex));
			OnPropertyChanged(nameof(SelectedMapIconItem));
		}
	}
}
