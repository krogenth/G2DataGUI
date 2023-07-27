using G2DataGUI.Common.Data.Moves;
using System;
using System.Collections.ObjectModel;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public sealed class MovesViewModel : BaseViewModel
{
    public ObservableCollection<Move> GameMoves { get; private set; } = Moves.Instance.GameMoves;
    private int _selectedMoveIndex;
    private Move _selectedMoveItem;

    public static MovesViewModel Instance { get; private set; } = new();

    private MovesViewModel()
    {
		SelectedMoveIndex = 0;
		Moves.Instance.CollectionRefreshed += MovesCollectionRefreshed;
    }

	private void MovesCollectionRefreshed(object sender, EventArgs _) =>
		SelectedMoveIndex = SelectedMoveIndex;

	public int SelectedMoveIndex
    {
        get => _selectedMoveIndex;
        set
        {
            if (value < 0)
			{
				value = 0;
			}

			if (value >= GameMoves.Count)
			{
				return;
			}

			_selectedMoveIndex = value;
            SelectedMoveItem = GameMoves[value];
            OnPropertyChanged(nameof(SelectedMoveIndex));
        }
    }

    public Move SelectedMoveItem
    {
        get => _selectedMoveItem;
        set
        {
            _selectedMoveItem = value;
            OnPropertyChanged(nameof(SelectedMoveItem));
        }
    }
}
