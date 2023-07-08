using G2DataGUI.Common.Data.Moves;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public sealed class SpecialsViewModel : BaseViewModel
{
    public ObservableCollection<SpecialSet> GameSpecialSets { get; private set; } = SpecialSets.Instance.GameSpecialSets;
    public ObservableCollection<Move> GameMoves { get; private set; } = Moves.Instance.GameMoves;
    private int _selectedSpecialSetIndex = 0;
    private SpecialSet _selectedSpecialSetItem;

    public static SpecialsViewModel Instance { get; private set; } = new();

    private SpecialsViewModel()
    {
        _selectedSpecialSetItem = GameSpecialSets.First();
        SpecialSets.Instance.CollectionRefreshed += SpecialSetCollectionRefreshed;
    }

	private void SpecialSetCollectionRefreshed(object sender, EventArgs _) =>
        SelectedSpecialSetItem = GameSpecialSets[SelectedSpecialSetIndex];

	public int SelectedSpecialSetIndex
    {
        get => _selectedSpecialSetIndex;
        set
        {
            if (value < 0)
			{
				value = 0;
			}

			if (value >= GameSpecialSets.Count)
			{
				return;
			}

			_selectedSpecialSetIndex = value;
            SelectedSpecialSetItem = GameSpecialSets[value];
            OnPropertyChanged(nameof(SelectedSpecialSetIndex));
        }
    }

    public SpecialSet SelectedSpecialSetItem
    {
        get => _selectedSpecialSetItem;
        set
        {
            _selectedSpecialSetItem = value;
            OnPropertyChanged(nameof(SelectedSpecialSetItem));
        }
    }
}