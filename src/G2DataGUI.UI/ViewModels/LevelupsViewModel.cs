using System;
using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Levelups;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class LevelupsViewModel : BaseViewModel
{
	public ObservableCollection<CharacterLevelups> GameLevelups { get; set; } = Levelups.Instance.GameLevelups;
	private int _selectedLevelupsIndex = 0;
	public CharacterLevelups? SelectedLevelupsItem { get; set; }
	public CharacterLevelupsViewModel SelectedCharacterLevelupsViewModel { get; set; } = new([]);

	public LevelupsViewModel()
	{
		SelectedLevelupsIndex = 0;
		Levelups.Instance.CollectionRefreshed += LevelupsCollectionRefreshed;
	}

	private void LevelupsCollectionRefreshed(object? sender, EventArgs _) =>
		SelectedLevelupsIndex = SelectedLevelupsIndex;

	public int SelectedLevelupsIndex
	{
		get => _selectedLevelupsIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= GameLevelups.Count)
			{
				return;
			}

			_selectedLevelupsIndex = value;
			SelectedLevelupsItem = GameLevelups[value];
			SelectedCharacterLevelupsViewModel = new(SelectedLevelupsItem.Levelups);
			OnPropertyChanged(nameof(SelectedLevelupsIndex));
			OnPropertyChanged(nameof(SelectedLevelupsItem));
			OnPropertyChanged(nameof(SelectedCharacterLevelupsViewModel));
		}
	}
}
