using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class BossMovesetsViewModel : BaseViewModel
{
	private EnemyMove[] _selectedBossMoves;
	private int _selectedBossMoveIndex;
	private EnemyMove _selectedBossMoveItem;

	public static BossMovesetsViewModel Instance { get; private set; } = new();

	private BossMovesetsViewModel()
	{
		SelectedBossMoves = new EnemyMove[EnemyMoveset.NumberOfMoves];
		SelectedBossMoveIndex = 0;
	}

	public EnemyMove[] SelectedBossMoves
	{
		get => _selectedBossMoves;
		set
		{
			_selectedBossMoves = value == null ? new EnemyMove[EnemyMoveset.NumberOfMoves] : value;
			SelectedBossMoveItem = SelectedBossMoves[SelectedBossMoveIndex];
			OnPropertyChanged(nameof(SelectedBossMoves));
		}
	}

	public int SelectedBossMoveIndex
	{
		get => _selectedBossMoveIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= SelectedBossMoves.Length)
			{
				return;
			}

			_selectedBossMoveIndex = value;
			SelectedBossMoveItem = SelectedBossMoves[value];
			OnPropertyChanged(nameof(SelectedBossMoveIndex));
		}
	}

	public EnemyMove SelectedBossMoveItem
	{
		get => _selectedBossMoveItem;
		set
		{
			_selectedBossMoveItem = value == null ? new EnemyMove() : value;
			OnPropertyChanged(nameof(SelectedBossMoveItem));
		}
	}
}
