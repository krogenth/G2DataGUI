using System.Collections.Generic;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapDialogueViewModel : BaseViewModel
{
	private List<List<IMapDialogueOpcode>> _selectedMapDialogueOpcodes = new();
	private int _selectedMapDialogueOpcodeIndex;
	private List<IMapDialogueOpcode> _selectedMapDialogueOpcodeItem;

	public static MapDialogueViewModel Instance { get; private set; } = new();

	private MapDialogueViewModel()
	{
		SelectedMapDialogueOpcodeIndex = 0;
	}

	public List<List<IMapDialogueOpcode>> SelectedMapDialogueOpcodes
	{
		get => _selectedMapDialogueOpcodes;
		set
		{
			_selectedMapDialogueOpcodes = value == null ? new List<List<IMapDialogueOpcode>>() : value;
			SelectedMapDialogueOpcodeIndex = 0;
			OnPropertyChanged(nameof(SelectedMapDialogueOpcodes));
			OnPropertyChanged(nameof(HasDialogueOpcodes));
		}
	}

	public bool HasDialogueOpcodes => _selectedMapDialogueOpcodes != null;

	public int SelectedMapDialogueOpcodeIndex
	{
		get => _selectedMapDialogueOpcodeIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= SelectedMapDialogueOpcodes.Count)
			{
				return;
			}

			_selectedMapDialogueOpcodeIndex = value;
			SelectedMapDialogueOpcodeItem = SelectedMapDialogueOpcodes[value];
			OnPropertyChanged(nameof(SelectedMapDialogueOpcodeIndex));
		}
	}

	public List<IMapDialogueOpcode> SelectedMapDialogueOpcodeItem
	{
		get => _selectedMapDialogueOpcodeItem;
		set
		{
			_selectedMapDialogueOpcodeItem = value;
			OnPropertyChanged(nameof(SelectedMapDialogueOpcodeItem));
		}
	}
}
