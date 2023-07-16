using System;
using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Common.Data.Bosses;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;
public class BossesViewModel : BaseViewModel
{
	public ObservableCollection<Boss> GameBosses { get; private set; } = Bosses.Instance.GameBosses;
	private int _selectedBossIndex;
	private Boss _selectedBossItem;
	private readonly BossStatsViewModel _bossStatsViewModel = BossStatsViewModel.Instance;
	private readonly BossAISectionViewModel _bossAISectionViewModel = BossAISectionViewModel.Instance;
	private readonly BossMovesetsViewModel _bossMovesetsViewModel = BossMovesetsViewModel.Instance;

	public static BossesViewModel Instance { get; } = new();

	private BossesViewModel()
	{
		SelectedBossIndex = 0;
		Bosses.Instance.CollectionRefreshed += BossesCollectionRefreshed;
	}

	private void BossesCollectionRefreshed(object sender, EventArgs _) =>
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

			if (value >= GameBosses.Count)
			{
				return;
			}

			_selectedBossIndex = value;
			SelectedBossItem = GameBosses[value];
			OnPropertyChanged(nameof(SelectedBossIndex));
		}
	}

	public Boss SelectedBossItem
	{
		get => _selectedBossItem;
		set
		{
			_selectedBossItem = value;
			_bossStatsViewModel.SelectedBossStats = value.Stats;
			_bossAISectionViewModel.SelectedBossAISection = value.AISection;
			_bossMovesetsViewModel.SelectedBossMoves = value.Movesets.First()?.Moves;
			OnPropertyChanged(nameof(SelectedBossItem));
		}
	}
}
