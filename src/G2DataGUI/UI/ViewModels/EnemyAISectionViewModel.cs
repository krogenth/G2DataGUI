using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class EnemyAISectionViewModel : BaseViewModel
{
    private EnemyAISection _selectedEnemyAISection = new();

    public static EnemyAISectionViewModel Instance { get; } = new();

    public EnemyAISection SelectedEnemyAISection
    {
        get => _selectedEnemyAISection;
        set
        {
            _selectedEnemyAISection = value == null ? new EnemyAISection() : value;
			OnPropertyChanged(nameof(SelectedEnemyAISection));
        }
    }
}