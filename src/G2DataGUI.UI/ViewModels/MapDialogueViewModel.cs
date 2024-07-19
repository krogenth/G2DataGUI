using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapDialogueViewModel : BaseViewModel
{
	public ObservableCollection<ObservableCollection<IMapDialogueOpcode>> SelectedMapDialogueOpcodes { get; set; } = [];
	private int _selectedMapDialogueOpcodeIndex;
	public ObservableCollection<IMapDialogueOpcode> SelectedMapDialogueOpcodeItem { get; set; } = [];

	public MapDialogueViewModel(ObservableCollection<ObservableCollection<IMapDialogueOpcode>> opcodes)
	{
		SelectedMapDialogueOpcodes = opcodes;
		SelectedMapDialogueOpcodeIndex = 0;
	}

	public int SelectedMapDialogueOpcodeIndex
	{
		get => _selectedMapDialogueOpcodeIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (SelectedMapDialogueOpcodes.Count == 0)
			{
				return;
			}

			_selectedMapDialogueOpcodeIndex = value;
			SelectedMapDialogueOpcodeItem = SelectedMapDialogueOpcodes[value];
			OnPropertyChanged(nameof(SelectedMapDialogueOpcodeIndex));
			OnPropertyChanged(nameof(SelectedMapDialogueOpcodeItem));
		}
	}
}
