using System.Collections.Generic;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapHTAsViewModel : BaseViewModel
{
	private List<MapHTA> _selectedMapHTAs = new();
	private int _selectedMapHTAIndex;
	private MapHTA _selectedMapHTAItem;

	public static MapHTAsViewModel Instance { get; private set; } = new();

	private MapHTAsViewModel()
	{
		SelectedMapHTAIndex = 0;
	}

	public List<MapHTA> SelectedMapHTAs
	{
		get => _selectedMapHTAs;
		set
		{
			_selectedMapHTAs = value == null ? new List<MapHTA>() : value;
			SelectedMapHTAIndex = 0;
			OnPropertyChanged(nameof(SelectedMapHTAs));
			OnPropertyChanged(nameof(HasMapHTAs));
		}
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

			if (value >= SelectedMapHTAs.Count)
			{
				return;
			}

			_selectedMapHTAIndex = value;
			SelectedMapHTAItem = SelectedMapHTAs[value];
			OnPropertyChanged(nameof(SelectedMapHTAIndex));
		}
	}

	public MapHTA SelectedMapHTAItem
	{
		get => _selectedMapHTAItem;
		set
		{
			_selectedMapHTAItem = value;
			OnPropertyChanged(nameof(SelectedMapHTAItem));
		}
	}
}
