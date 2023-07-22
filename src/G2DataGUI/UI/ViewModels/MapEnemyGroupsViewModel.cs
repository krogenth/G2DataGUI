using System.Collections.Generic;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapEnemyGroupsViewModel : BaseViewModel
{
	private List<MapEnemyGroup> _selectedMapEnemyGroups = new();
	private int _selectedMapEnemyGroupIndex;
	private MapEnemyGroup _selectedMapEnemyGroupItem;

	public static MapEnemyGroupsViewModel Instance { get; private set; } = new();

	private MapEnemyGroupsViewModel()
	{
		SelectedMapEnemyGroupIndex = 0;
	}

	public List<MapEnemyGroup> SelectedMapEnemyGroups
	{
		get => _selectedMapEnemyGroups;
		set
		{
			_selectedMapEnemyGroups = value == null ? new List<MapEnemyGroup>() : value;
			SelectedMapEnemyGroupIndex = 0;
			OnPropertyChanged(nameof(SelectedMapEnemyGroups));
			OnPropertyChanged(nameof(HasMapEnemyGroups));
		}
	}

	public bool HasMapEnemyGroups { get => SelectedMapEnemyGroups.Count > 0; }

	public int SelectedMapEnemyGroupIndex
	{
		get => _selectedMapEnemyGroupIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (value >= SelectedMapEnemyGroups.Count)
			{
				return;
			}

			_selectedMapEnemyGroupIndex = value;
			SelectedMapEnemyGroupItem = SelectedMapEnemyGroups[value];
			OnPropertyChanged(nameof(SelectedMapEnemyGroupIndex));
		}
	}

	public MapEnemyGroup SelectedMapEnemyGroupItem
	{
		get => _selectedMapEnemyGroupItem;
		set
		{
			_selectedMapEnemyGroupItem = value;
			OnPropertyChanged(nameof(SelectedMapEnemyGroupItem));
		}
	}
}
