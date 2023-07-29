using System.Collections.Generic;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapScriptsViewModel : BaseViewModel
{
	private List<MapScript> _selectedMapScripts = new();
	private int _selectedMapScriptIndex;
	private MapScript _selectedMapScriptItem;

	public static MapScriptsViewModel Instance { get; private set; } = new();

	private MapScriptsViewModel()
	{

	}

	public List<MapScript> SelectedMapScripts
	{
		get => _selectedMapScripts;
		set
		{
			_selectedMapScripts = value == null ? new List<MapScript>() : value;
			SelectedMapScriptIndex = 0;
			OnPropertyChanged(nameof(SelectedMapScripts));
			OnPropertyChanged(nameof(HasMapScripts));
			OnPropertyChanged(nameof(SelectedMapScriptIndex));
		}
	}

	public bool HasMapScripts => SelectedMapScripts.Count > 0;

	public int SelectedMapScriptIndex
	{
		get => _selectedMapScriptIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= SelectedMapScripts.Count)
			{
				return;
			}

			_selectedMapScriptIndex = value;
			SelectedMapScriptItem = SelectedMapScripts[value];
		}
	}

	public MapScript SelectedMapScriptItem
	{
		get => _selectedMapScriptItem;
		set
		{
			_selectedMapScriptItem = value;
			OnPropertyChanged(nameof(SelectedMapScriptItem));
		}
	}
}
