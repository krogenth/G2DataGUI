using System;
using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Levelups;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class LevelupsViewModel : BaseViewModel
{
    public ObservableCollection<CharacterLevelups> GameLevelups { get; private set; } = Levelups.Instance.GameLevelups;
    private int _selectedLevelupsIndex;
    private CharacterLevelups _selectedLevelupsItem;
    private readonly CharacterLevelupsViewModel _characterLevelupsViewModel = CharacterLevelupsViewModel.Instance;

    public static LevelupsViewModel Instance { get; private set; } = new();

    private LevelupsViewModel()
    {
        SelectedLevelupsIndex = 0;
        Levelups.Instance.CollectionRefreshed += LevelupsCollectionRefreshed;
    }

    private void LevelupsCollectionRefreshed(object sender, EventArgs _) =>
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
            OnPropertyChanged(nameof(SelectedLevelupsIndex));
        }
    }

    public CharacterLevelups SelectedLevelupsItem
    {
        get => _selectedLevelupsItem;
        set
        {
            _selectedLevelupsItem = value;
            _characterLevelupsViewModel.SelectedCharacterLevelups = SelectedLevelupsItem.Levelups;
            OnPropertyChanged(nameof(SelectedLevelupsItem));
        }
    }
}
