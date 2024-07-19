using G2DataGUI.Common.Data.Levelups;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class CharacterLevelupsViewModel : BaseViewModel
{
	public LevelupStats[] SelectedCharacterLevelups { get; set; }
	private int _selectedCharacterLevelupIndex;
	public LevelupStats SelectedCharacterLevelupItem { get; set; } = new();

	public CharacterLevelupsViewModel(LevelupStats[] levelups)
	{
		SelectedCharacterLevelups = levelups;
		SelectedCharacterLevelupIndex = 0;
	}

	public int SelectedCharacterLevelupIndex
	{
		get => _selectedCharacterLevelupIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}
			else if (value >= SelectedCharacterLevelups.Length)
			{
				value = SelectedCharacterLevelups.Length - 1;
			}

			if (SelectedCharacterLevelups.Length == 0)
			{
				return;
			}

			_selectedCharacterLevelupIndex = value;
			SelectedCharacterLevelupItem = SelectedCharacterLevelups[value];
			OnPropertyChanged(nameof(SelectedCharacterLevelupIndex));
			OnPropertyChanged(nameof(SelectedCharacterLevelupItem));
		}
	}
}
