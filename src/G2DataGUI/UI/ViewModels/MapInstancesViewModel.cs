using System.Collections.Generic;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapInstancesViewModel : BaseViewModel
{
	private List<MapInstance> _selectedMapInstances = new();
	private int _selectedMapInstanceIndex;
	private MapInstance _selectedMapInstanceItem;

	public static MapInstancesViewModel Instance { get; private set; } = new();

	private MapInstancesViewModel()
	{
		SelectedMapInstanceIndex = 0;
	}

	public List<MapInstance> SelectedMapInstances
	{
		get => _selectedMapInstances;
		set
		{
			_selectedMapInstances = value == null ? new List<MapInstance>() : value;
			SelectedMapInstanceIndex = 0;
			OnPropertyChanged(nameof(SelectedMapInstances));
			OnPropertyChanged(nameof(HasMapInstances));
		}
	}

	public bool HasMapInstances => SelectedMapInstances.Count > 0;

	public int SelectedMapInstanceIndex
	{
		get => _selectedMapInstanceIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= SelectedMapInstances.Count)
			{
				return;
			}

			_selectedMapInstanceIndex = value;
			SelectedMapInstanceItem = SelectedMapInstances[value];
			OnPropertyChanged(nameof(SelectedMapInstanceIndex));
		}
	}

	public MapInstance SelectedMapInstanceItem
	{
		get => _selectedMapInstanceItem;
		set
		{
			_selectedMapInstanceItem = value;
			OnPropertyChanged(nameof(SelectedMapInstanceItem));
		}
	}
}
