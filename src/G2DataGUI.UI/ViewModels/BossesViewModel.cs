using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Bosses;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;
public class BossesViewModel : BaseViewModel
{
	public ObservableCollection<Boss> GameBosses { get; set; } = Bosses.Instance.GameBosses;
	private int _selectedBossIndex = 0;
	public Boss? SelectedBossItem { get; set; }
	public BossStatsViewModel SelectedBossStatsViewModel = new(new());
	public BossAISectionViewModel SelectedBossAISectionViewModel = new(new());
	public BossMovesetsViewModel SelectedBossMovesetsViewModel = new([]);

	public BossesViewModel()
	{
		SelectedBossIndex = 0;
		Bosses.Instance.CollectionRefreshed += BossesCollectionRefreshed;
	}

	private void BossesCollectionRefreshed(object? sender, EventArgs _) =>
		SelectedBossIndex = SelectedBossIndex;

	public int SelectedBossIndex
	{
		get => _selectedBossIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			else if (value >= GameBosses.Count)
			{
				value -= GameBosses.Count - 1;
			}

			if (GameBosses.Count == 0)
			{
				return;
			}

			_selectedBossIndex = value;
			SelectedBossItem = GameBosses[value];
			SelectedBossStatsViewModel = new(SelectedBossItem.Stats);
			SelectedBossAISectionViewModel = new(SelectedBossItem.AISection);
			SelectedBossMovesetsViewModel = new(SelectedBossItem.Movesets.First().Moves);
			OnPropertyChanged(nameof(SelectedBossIndex));
			OnPropertyChanged(nameof(SelectedBossItem));
		}
	}
}
