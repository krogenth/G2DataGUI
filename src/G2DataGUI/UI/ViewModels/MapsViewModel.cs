using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;
using System;

namespace G2DataGUI.UI.ViewModels;

public sealed class MapsViewModel : BaseViewModel
{
    public ObservableCollection<Map> GameMaps { get; private set; } = Maps.Instance.GameMaps;
    private int _selectedMapIndex;
    private Map _selectedMapItem;
	private readonly MapEntriesViewModel _entriesViewModel = MapEntriesViewModel.Instance;
	private readonly MapInstancesViewModel _instancesViewModel = MapInstancesViewModel.Instance;
	private readonly MapHTAsViewModel _htaViewModel = MapHTAsViewModel.Instance;
	private readonly MapScriptsViewModel _scriptsViewModel = MapScriptsViewModel.Instance;
	private readonly MapEnemyPositionsViewModel _enemyPositionsViewModel = MapEnemyPositionsViewModel.Instance;
	private readonly MapEnemyGroupsViewModel _enemyGroupsViewModel = MapEnemyGroupsViewModel.Instance;
	private readonly MapMOSsViewModel _mosViewModel = MapMOSsViewModel.Instance;
	private readonly MapIconsViewModel _iconsViewModel = MapIconsViewModel.Instance;
	private readonly MapShopViewModel _shopViewModel = MapShopViewModel.Instance;

	public static MapsViewModel Instance { get; private set; } = new();

    private MapsViewModel()
    {
        SelectedMapIndex = 0;
        Maps.Instance.CollectionRefreshed += MapsCollectionRefreshed;
    }

    private void MapsCollectionRefreshed(object sender, EventArgs _) =>
        SelectedMapIndex = SelectedMapIndex;

    public int SelectedMapIndex
    {
        get => _selectedMapIndex;
        set
        {
            if (value < 0)
            {
                value = 0;
            }

            if (value >= GameMaps.Count)
            {
                return;
            }

            _selectedMapIndex = value;
            SelectedMapItem = GameMaps[value];
            OnPropertyChanged(nameof(SelectedMapIndex));
        }
    }

    public Map SelectedMapItem
    {
        get => _selectedMapItem;
        set
        {
            _selectedMapItem = value;
			_entriesViewModel.SelectedMapEntries = value.Entries;
			_instancesViewModel.SelectedMapInstances = value.Instances;
			_htaViewModel.SelectedMapHTAs = value.HTAs;
			_scriptsViewModel.SelectedMapScripts = value.Scripts;
			_enemyPositionsViewModel.SelectedMapEnemyPositions = value.EnemyPositions;
			_enemyGroupsViewModel.SelectedMapEnemyGroups = value.EnemyGroups;
			_mosViewModel.SelectedMapMOSs = value.MOSs;
			_iconsViewModel.SelectedMapIcons = value.Icons;
			_shopViewModel.SelectedMapShop = value.Shop;
            OnPropertyChanged(nameof(SelectedMapItem));
        }
    }
}
