using System;
using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.Common.Data.Starting;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;
public class StartingStatsViewModel : BaseViewModel
{
	public ObservableCollection<CharacterStartingStats> GameStartingStats { get; set; } = StartingStats.Instance.GameStartingStats;
	public ObservableCollection<Item> GameItems { get; set; } = Items.Instance.GameItems;
	private int _selectedStartingStatsIndex = 0;
	public CharacterStartingStats? SelectedStartingStatsItem { get; set; }

	public StartingStatsViewModel()
	{
		SelectedStartingStatsIndex = 0;
		StartingStats.Instance.CollectionRefreshed += StartingStatsCollectionRefreshed;
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
	}

	private void StartingStatsCollectionRefreshed(object? sender, EventArgs _) =>
		SelectedStartingStatsIndex = SelectedStartingStatsIndex;

	private void ItemsCollectionRefreshed(object? sender, EventArgs _) =>
		OnPropertyChanged(nameof(GameItems));

	public int SelectedStartingStatsIndex
	{
		get => _selectedStartingStatsIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= GameStartingStats.Count)
			{
				return;
			}

			_selectedStartingStatsIndex = value;
			SelectedStartingStatsItem = GameStartingStats[value];
			OnPropertyChanged(nameof(SelectedStartingStatsIndex));
			OnPropertyChanged(nameof(SelectedStartingStatsItem));
		}
	}
}
