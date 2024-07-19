using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapHTAsViewModel : BaseViewModel
{
	public List<MapHTA> SelectedMapHTAs { get; set; } = [];
	private int _selectedMapHTAIndex = 0;
	public MapHTA SelectedMapHTAItem { get; set; } = new();

	public MapHTAsViewModel(List<MapHTA> htas)
	{
		SelectedMapHTAs = htas;
		SelectedMapHTAIndex = 0;
	}

	public bool HasMapHTAs => SelectedMapHTAs.Count > 0;

	public int SelectedMapHTAIndex
	{
		get => _selectedMapHTAIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (SelectedMapHTAs.Count == 0)
			{
				return;
			}

			_selectedMapHTAIndex = value;
			SelectedMapHTAItem = SelectedMapHTAs[value];
			OnPropertyChanged(nameof(SelectedMapHTAIndex));
			OnPropertyChanged(nameof(SelectedMapHTAItem));
		}
	}
}
