using System.Collections.Generic;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapMOSsViewModel : BaseViewModel
{
	private List<MapMOS> _selectedMapMOSs = new();
	private int _selectedMapMOSIndex;
	private MapMOS _selectedMapMOSItem;

	public static MapMOSsViewModel Instance { get; private set; } = new();

	private MapMOSsViewModel()
	{
		SelectedMapMOSIndex = 0;
	}

	public List<MapMOS> SelectedMapMOSs
	{
		get => _selectedMapMOSs;
		set
		{
			_selectedMapMOSs = value == null ? new List<MapMOS>() : value;
			SelectedMapMOSIndex = 0;
			OnPropertyChanged(nameof(SelectedMapMOSs));
			OnPropertyChanged(nameof(HasMapMOSs));
		}
	}

	public bool HasMapMOSs { get => SelectedMapMOSs.Count > 0; }

	public int SelectedMapMOSIndex
	{
		get => _selectedMapMOSIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= SelectedMapMOSs.Count)
			{
				return;
			}

			_selectedMapMOSIndex = value;
			SelectedMapMOSItem = SelectedMapMOSs[value];
			OnPropertyChanged(nameof(SelectedMapMOSIndex));
		}
	}

	public MapMOS SelectedMapMOSItem
	{
		get => _selectedMapMOSItem;
		set
		{
			_selectedMapMOSItem = value;
			OnPropertyChanged(nameof(SelectedMapMOSItem));
		}
	}
}
