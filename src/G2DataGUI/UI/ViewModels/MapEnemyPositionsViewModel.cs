using System.Collections.Generic;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapEnemyPositionsViewModel : BaseViewModel
{
	private List<MapEnemyPosition> _selectedMapEnemyPositions = new();
	private int _selectedMapEnemyPositionIndex;
	private MapEnemyPosition _selectedMapEnemyPositionItem;

	public static MapEnemyPositionsViewModel Instance { get; private set; } = new();

	private MapEnemyPositionsViewModel()
	{
		SelectedMapEnemyPositionIndex = 0;
	}

	public List<MapEnemyPosition> SelectedMapEnemyPositions
	{
		get => _selectedMapEnemyPositions;
		set
		{
			_selectedMapEnemyPositions = value == null ? new List<MapEnemyPosition>() : value;
			SelectedMapEnemyPositionIndex = 0;
			OnPropertyChanged(nameof(SelectedMapEnemyPositions));
			OnPropertyChanged(nameof(HasMapEnemyPositions));
		}
	}

	public bool HasMapEnemyPositions { get => SelectedMapEnemyPositions.Count > 0; }

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
		}
	}

	public MapEnemyPosition SelectedMapEnemyPositionItem
	{
		get => _selectedMapEnemyPositionItem;
		set
		{
			_selectedMapEnemyPositionItem = value;
			OnPropertyChanged(nameof(SelectedMapEnemyPositionItem));
		}
	}
}
