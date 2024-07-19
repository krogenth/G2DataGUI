using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapInstancesViewModel : BaseViewModel
{
	public List<MapInstance> SelectedMapInstances { get; set; } = [];
	private int _selectedMapInstanceIndex = 0;
	public MapInstance SelectedMapInstanceItem { get; set; } = new();

	public MapInstancesViewModel(List<MapInstance> instances)
	{
		SelectedMapInstances = instances;
		SelectedMapInstanceIndex = 0;
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

			if (SelectedMapInstances.Count == 0)
			{
				return;
			}

			_selectedMapInstanceIndex = value;
			SelectedMapInstanceItem = SelectedMapInstances[value];
			OnPropertyChanged(nameof(SelectedMapInstanceIndex));
			OnPropertyChanged(nameof(SelectedMapInstanceItem));
		}
	}
}
