using System.Collections.Generic;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapEntriesViewModel : BaseViewModel
{
	private List<MapEntry> _selectedMapEntries = new();
	private int _selectedMapEntryIndex;
	private MapEntry _selectedMapEntryItem;

	public static MapEntriesViewModel Instance { get; private set; } = new();

	private MapEntriesViewModel()
	{
		SelectedMapEntryIndex = 0;
	}

	public List<MapEntry> SelectedMapEntries
	{
		get => _selectedMapEntries;
		set
		{
			_selectedMapEntries = value == null ? new List<MapEntry>() : value;
			SelectedMapEntryIndex = 0;
			OnPropertyChanged(nameof(SelectedMapEntries));
			OnPropertyChanged(nameof(HasMapEntries));
		}
	}

	public bool HasMapEntries { get => SelectedMapEntries.Count > 0; }

	public int SelectedMapEntryIndex
	{
		get => _selectedMapEntryIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= SelectedMapEntries.Count)
			{
				return;
			}

			_selectedMapEntryIndex = value;
			SelectedMapEntryItem = SelectedMapEntries[value];
			OnPropertyChanged(nameof(SelectedMapEntryIndex));
		}
	}

	public MapEntry SelectedMapEntryItem
	{
		get => _selectedMapEntryItem;
		set
		{
			_selectedMapEntryItem = value;
			OnPropertyChanged(nameof(SelectedMapEntryItem));
		}
	}
}
