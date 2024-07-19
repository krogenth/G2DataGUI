using G2DataGUI.Common.Data.Moves;
using System;
using System.Collections.ObjectModel;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public sealed class SpecialsViewModel : BaseViewModel
{
	public ObservableCollection<SpecialSet> GameSpecialSets { get; set; } = SpecialSets.Instance.GameSpecialSets;
	public ObservableCollection<Move> GameMoves { get; set; } = Moves.Instance.GameMoves;
	private int _selectedSpecialSetIndex = 0;
	public SpecialSet? SelectedSpecialSetItem { get; set; }

	public SpecialsViewModel()
	{
		SelectedSpecialSetIndex = 0;
		SpecialSets.Instance.CollectionRefreshed += SpecialSetCollectionRefreshed;
	}

	private void SpecialSetCollectionRefreshed(object? sender, EventArgs _) =>
		SelectedSpecialSetIndex = SelectedSpecialSetIndex;

	public int SelectedSpecialSetIndex
	{
		get => _selectedSpecialSetIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (GameSpecialSets.Count == 0)
			{
				return;
			}

			_selectedSpecialSetIndex = value;
			SelectedSpecialSetItem = GameSpecialSets[value];
			OnPropertyChanged(nameof(SelectedSpecialSetIndex));
			OnPropertyChanged(nameof(SelectedSpecialSetItem));
		}
	}
}
