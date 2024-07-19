using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;
public class BossAISectionViewModel(EnemyAISection ai) : BaseViewModel
{
	public EnemyAISection SelectedBossAISection { get; set; } = ai;
}
