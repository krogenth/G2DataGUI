using G2DataGUI.Common.Data.Moves;
using System.Collections.ObjectModel;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public sealed class MovesViewModel : BaseViewModel
{
	public ObservableCollection<Move> GameMoves { get; set; } = Moves.Instance.GameMoves;
	private int _selectedMoveIndex = 0;
	public Move? SelectedMoveItem { get; set; }

	public MovesViewModel()
	{
		SelectedMoveIndex = 0;
		Moves.Instance.CollectionRefreshed += MovesCollectionRefreshed;
	}

	private void MovesCollectionRefreshed(object? sender, EventArgs _) =>
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

			if (GameMoves.Count == 0)
			{
				return;
			}

			_selectedMoveIndex = value;
			SelectedMoveItem = GameMoves[value];
			OnPropertyChanged(nameof(SelectedMoveIndex));
			OnPropertyChanged(nameof(SelectedMoveItem));
		}
	}
}
