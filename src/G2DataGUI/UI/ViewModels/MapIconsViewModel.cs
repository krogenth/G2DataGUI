using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapIconsViewModel : BaseViewModel
{
	public ObservableCollection<Item> GameItems { get; private set; } = Items.Instance.GameItems;
	private List<MapIcon> _selectedMapIcons = new();
	private int _selectedMapIconIndex;
	private MapIcon _selectedMapIconItem;

	public static MapIconsViewModel Instance { get; private set; } = new();

	private MapIconsViewModel()
	{
		SelectedMapIconIndex = 0;
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
	}

	private void ItemsCollectionRefreshed(object sender, EventArgs e) =>
		OnPropertyChanged(nameof(GameItems));

	public List<MapIcon> SelectedMapIcons
	{
		get => _selectedMapIcons;
		set
		{
			_selectedMapIcons = value == null ? new List<MapIcon>() : value;
			SelectedMapIconIndex = 0;
			OnPropertyChanged(nameof(SelectedMapIcons));
			OnPropertyChanged(nameof(HasMapIcons));
		}
	}

	public bool HasMapIcons { get => SelectedMapIcons.Count > 0; }

	public int SelectedMapIconIndex
	{
		get => _selectedMapIconIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= SelectedMapIcons.Count)
			{
				return;
			}

			_selectedMapIconIndex = value;
			SelectedMapIconItem = SelectedMapIcons[value];
			OnPropertyChanged(nameof(SelectedMapIconIndex));
		}
	}

	public MapIcon SelectedMapIconItem
	{
		get => _selectedMapIconItem;
		set
		{
			_selectedMapIconItem = value;
			OnPropertyChanged(nameof(SelectedMapIconItem));
		}
	}
}
