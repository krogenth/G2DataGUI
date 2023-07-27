using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class EnemyMovesetViewModel : BaseViewModel
{
	private EnemyMove[] _selectEnemyMoves;
	private int _selectedEnemyMoveIndex;
	private EnemyMove _selectedEnemyMoveItem;

    public static EnemyMovesetViewModel Instance { get; } = new();

	private EnemyMovesetViewModel()
	{
		SelectedEnemyMoves = new EnemyMove[EnemyMoveset.NumberOfMoves];
		SelectedEnemyMoveIndex = 0;
	}

	public EnemyMove[] SelectedEnemyMoves
	{
		get => _selectEnemyMoves;
		set
		{
			_selectEnemyMoves = value == null ? new EnemyMove[EnemyMoveset.NumberOfMoves] : value;
			SelectedEnemyMoveItem = SelectedEnemyMoves[SelectedEnemyMoveIndex];
			OnPropertyChanged(nameof(SelectedEnemyMoves));
		}
	}

	public int SelectedEnemyMoveIndex
	{
		get => _selectedEnemyMoveIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= SelectedEnemyMoves.Length)
			{
				return;
			}

			_selectedEnemyMoveIndex = value;
			SelectedEnemyMoveItem = SelectedEnemyMoves[value];
			OnPropertyChanged(nameof(SelectedEnemyMoveIndex));
		}
	}

	public EnemyMove SelectedEnemyMoveItem
	{
		get => _selectedEnemyMoveItem;
		set
		{
			_selectedEnemyMoveItem = value == null ? new EnemyMove() : value;
			OnPropertyChanged(nameof(SelectedEnemyMoveItem));
		}
	}
}
