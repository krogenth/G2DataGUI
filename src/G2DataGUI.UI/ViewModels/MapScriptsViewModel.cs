using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapScriptsViewModel : BaseViewModel
{
    public List<MapScript> SelectedMapScripts { get; set; } = [];
	private int _selectedMapScriptIndex = 0;
    public MapScript SelectedMapScriptItem { get; set; } = new();

    public MapScriptsViewModel(List<MapScript> scripts)
	{
		SelectedMapScripts = scripts;
	}

	public int SelectedMapScriptIndex
	{
		get => _selectedMapScriptIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
            }

            if (SelectedMapScripts.Count == 0)
			{
				return;
			}

			_selectedMapScriptIndex = value;
			SelectedMapScriptItem = SelectedMapScripts[value];
            OnPropertyChanged(nameof(SelectedMapScriptIndex));
            OnPropertyChanged(nameof(SelectedMapScriptItem));
        }
	}
}
