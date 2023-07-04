using G2DataGUI.Common.Data.Enemies;

namespace G2DataGUI.UI.ViewModels;

public class EnemyAISectionViewModel : BaseViewModel
{
    private EnemyAISection _selectedEnemyAISection = new EnemyAISection();

    public static EnemyAISectionViewModel Instance { get; } = new();

    public EnemyAISection SelectedEnemyAISection
    {
        get => _selectedEnemyAISection;
        set
        {
            if (value == null) _selectedEnemyAISection = new EnemyAISection();
            else _selectedEnemyAISection = value;
            OnPropertyChanged(nameof(SelectedEnemyAISection));
        }
    }
}