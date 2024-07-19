using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapEntriesViewModel : BaseViewModel
{
	public List<MapEntry> SelectedMapEntries { get; set; } = [];
	private int _selectedMapEntryIndex = 0;
	public MapEntry SelectedMapEntryItem { get; set; } = new();

	public MapEntriesViewModel(List<MapEntry> entries)
	{
		SelectedMapEntries = entries;
		SelectedMapEntryIndex = 0;
	}

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
			OnPropertyChanged(nameof(SelectedMapEntryItem));
		}
	}
}
