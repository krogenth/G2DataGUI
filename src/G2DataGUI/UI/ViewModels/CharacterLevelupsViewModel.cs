using G2DataGUI.Common.Data.Levelups;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class CharacterLevelupsViewModel : BaseViewModel
{
    private LevelupStats[] _selectedCharacterLevelups;
    private int _selectedCharacterLevelupIndex;
    private LevelupStats _selectedCharacterLevelupItem;

    public static CharacterLevelupsViewModel Instance { get; private set; } = new();

    private CharacterLevelupsViewModel()
    {
        SelectedCharacterLevelups = new LevelupStats[CharacterLevelups.LevelupsPerCharacter];
        SelectedCharacterLevelupIndex = 0;
    }

    public LevelupStats[] SelectedCharacterLevelups
    {
        get => _selectedCharacterLevelups;
        set
        {
            _selectedCharacterLevelups = value == null ? new LevelupStats[CharacterLevelups.LevelupsPerCharacter] : value;
            SelectedCharacterLevelupIndex = SelectedCharacterLevelupIndex;
            OnPropertyChanged(nameof(SelectedCharacterLevelups));
        }
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

            if (value >= SelectedCharacterLevelups.Length)
            {
                return;
            }

            _selectedCharacterLevelupIndex = value;
            SelectedCharacterLevelupItem = SelectedCharacterLevelups[value];
            OnPropertyChanged(nameof(SelectedCharacterLevelupIndex));
        }
    }

    public LevelupStats SelectedCharacterLevelupItem
    {
        get => _selectedCharacterLevelupItem;
        set
        {
            _selectedCharacterLevelupItem = value == null ? new LevelupStats() : value;
            OnPropertyChanged(nameof(SelectedCharacterLevelupItem));
        }
    }
}
