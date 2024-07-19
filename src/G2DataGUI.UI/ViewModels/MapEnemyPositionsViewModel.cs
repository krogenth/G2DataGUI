using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapEnemyPositionsViewModel : BaseViewModel
{
	public List<MapEnemyPosition> SelectedMapEnemyPositions { get; set; } = [];
	private int _selectedMapEnemyPositionIndex = 0;
	public MapEnemyPosition SelectedMapEnemyPositionItem { get; set; } = new();

	public MapEnemyPositionsViewModel(List<MapEnemyPosition> positions)
	{
		SelectedMapEnemyPositions = positions;
		SelectedMapEnemyPositionIndex = 0;
	}

	public int SelectedMapEnemyPositionIndex
	{
		get => _selectedMapEnemyPositionIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= SelectedMapEnemyPositions.Count)
			{
				return;
			}

			_selectedMapEnemyPositionIndex = value;
			SelectedMapEnemyPositionItem = SelectedMapEnemyPositions[value];
			OnPropertyChanged(nameof(SelectedMapEnemyPositionIndex));
			OnPropertyChanged(nameof(SelectedMapEnemyPositionItem));
		}
	}
}
