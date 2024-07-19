using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class MapEnemyGroupsViewModel : BaseViewModel
{
	public List<MapEnemyGroup> SelectedMapEnemyGroups { get; set; } = [];
	private int _selectedMapEnemyGroupIndex = 0;
	public MapEnemyGroup SelectedMapEnemyGroupItem { get; set; } = new();

	public MapEnemyGroupsViewModel(List<MapEnemyGroup> groups)
	{
		SelectedMapEnemyGroups = groups;
		SelectedMapEnemyGroupIndex = 0;
	}

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
			OnPropertyChanged(nameof(SelectedMapEnemyGroupItem));
		}
	}
}
