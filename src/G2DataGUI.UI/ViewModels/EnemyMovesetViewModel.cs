using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class EnemyMovesetViewModel : BaseViewModel
{
	public EnemyMoveset SelectedEnemyMoveset { get; set; } = new();
	private int _selectedEnemyMoveIndex;
	public EnemyMove SelectedEnemyMoveItem { get; set; } = new();

	public EnemyMovesetViewModel(EnemyMoveset moves)
	{
		SelectedEnemyMoveset = moves;
		SelectedEnemyMoveIndex = 0;
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
			else if (value >= EnemyMoveset.NumberOfMoves)
			{
				value = EnemyMoveset.NumberOfMoves - 1;
			}

			_selectedEnemyMoveIndex = value;
			SelectedEnemyMoveItem = SelectedEnemyMoveset.Moves[value];
			OnPropertyChanged(nameof(SelectedEnemyMoveIndex));
			OnPropertyChanged(nameof(SelectedEnemyMoveItem));
		}
	}
}
