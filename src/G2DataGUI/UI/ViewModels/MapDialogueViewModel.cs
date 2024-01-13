using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Maps.MapDialogueOpcodes;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapDialogueViewModel : BaseViewModel
{
	private ObservableCollection<ObservableCollection<IMapDialogueOpcode>> _selectedMapDialogueOpcodes = new();
	private int _selectedMapDialogueOpcodeIndex;
	private ObservableCollection<IMapDialogueOpcode> _selectedMapDialogueOpcodeItem;

	public static MapDialogueViewModel Instance { get; private set; } = new();

	private MapDialogueViewModel()
	{
		SelectedMapDialogueOpcodeIndex = 0;
	}

	public ObservableCollection<ObservableCollection<IMapDialogueOpcode>> SelectedMapDialogueOpcodes
	{
		get => _selectedMapDialogueOpcodes;
		set
		{
			_selectedMapDialogueOpcodes = value == null ? new ObservableCollection<ObservableCollection<IMapDialogueOpcode>>() : value;
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

	public ObservableCollection<IMapDialogueOpcode> SelectedMapDialogueOpcodeItem
	{
		get => _selectedMapDialogueOpcodeItem;
		set
		{
			_selectedMapDialogueOpcodeItem = value;
			OnPropertyChanged(nameof(SelectedMapDialogueOpcodeItem));
		}
	}
}
