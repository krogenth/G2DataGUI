using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;
public class BossAISectionViewModel : BaseViewModel
{
	private EnemyAISection _selectedBossAISection = new();
	public static BossAISectionViewModel Instance { get; private set; } = new();

	private BossAISectionViewModel() { }

	public EnemyAISection SelectedBossAISection
	{
		get => _selectedBossAISection;
		set
		{
			_selectedBossAISection = value == null ? new EnemyAISection() : value;
			OnPropertyChanged(nameof(SelectedBossAISection));
		}
	}
}
