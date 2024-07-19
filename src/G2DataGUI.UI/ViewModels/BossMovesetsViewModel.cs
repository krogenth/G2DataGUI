using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class BossMovesetsViewModel : BaseViewModel
{
	public EnemyMove[] SelectedBossMoves { get; set; } = [];
	public EnemyMove SelectedBossMoveItem { get; set; } = new();

	private int _selectedBossMoveIndex = 0;

	public BossMovesetsViewModel(EnemyMove[] moves)
	{
		SelectedBossMoves = moves;
		SelectedBossMoveIndex = 0;
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
			else if (value >= SelectedBossMoves.Length)
			{
				value = SelectedBossMoves.Length - 1;
			}

			if (SelectedBossMoves.Length == 0)
			{
				return;
			}

			_selectedBossMoveIndex = value;
			SelectedBossMoveItem = SelectedBossMoves[value];
			OnPropertyChanged(nameof(SelectedBossMoveIndex));
			OnPropertyChanged(nameof(SelectedBossMoveItem));
		}
	}
}
