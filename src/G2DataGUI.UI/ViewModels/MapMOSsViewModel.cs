using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapMOSsViewModel : BaseViewModel
{
	public List<MapMOS> SelectedMapMOSs { get; set; } = [];
	private int _selectedMapMOSIndex = 0;
	public MapMOS SelectedMapMOSItem { get; set; } = new();

	public MapMOSsViewModel(List<MapMOS> mos)
	{
		SelectedMapMOSs = mos;
		SelectedMapMOSIndex = 0;
	}

	public bool HasMapMOSs => SelectedMapMOSs.Count > 0;

	public int SelectedMapMOSIndex
	{
		get => _selectedMapMOSIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (SelectedMapMOSs.Count == 0)
			{
				return;
			}

			_selectedMapMOSIndex = value;
			SelectedMapMOSItem = SelectedMapMOSs[value];
			OnPropertyChanged(nameof(SelectedMapMOSIndex));
			OnPropertyChanged(nameof(SelectedMapMOSItem));
		}
	}
}
