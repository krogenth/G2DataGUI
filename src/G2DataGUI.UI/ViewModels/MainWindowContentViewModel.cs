using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public sealed class MainWindowContentViewModel : BaseViewModel
{
	public MovesViewModel MovesViewModel { get; set; } = new();
	public ManaeggsViewModel ManaeggsViewModel { get; set; } = new();
	public SkillsViewModel SkillsViewModel { get; set; } = new();
	public SkillbooksViewModel SkillbooksViewModel { get; set; } = new();
	public SpecialsViewModel SpecialsViewModel { get; set; } = new();
	public ItemsViewModel ItemsViewModel { get; set; } = new();
	public StartingStatsViewModel StartingStatsViewModel { get; set; } = new();
	public LevelupsViewModel LevelupsViewModel { get; set; } = new();
	public EnemiesViewModel EnemiesViewModel { get; set; } = new();
	public BossesViewModel BossesViewModel { get; set; } = new();
	public MapsViewModel MapsViewModel { get; set; } = new();
}
