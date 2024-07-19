using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.UI.Common.ViewModels;
using System;

namespace G2DataGUI.UI.ViewModels;

public sealed class MapsViewModel : BaseViewModel
{
	public ObservableCollection<Map> GameMaps { get; set; } = Maps.Instance.GameMaps;
	private int _selectedMapIndex = 0;
	public Map? SelectedMapItem { get; set; }
	public MapEntriesViewModel SelectedMapEntriesViewModel { get; set; } = new([]);
	public MapInstancesViewModel SelectedMapInstancesViewModel { get; set; } = new([]);
	public MapHTAsViewModel SelectedMapHtaViewModel { get; set; } = new([]);
	public MapScriptsViewModel SelectedMapScriptsViewModel { get; set; } = new([]);
	public MapEnemyPositionsViewModel SelectedMapEnemyPositionsViewModel { get; set; } = new([]);
	public MapEnemyGroupsViewModel SelectedMapEnemyGroupsViewModel { get; set; } = new([]);
	public MapMOSsViewModel SelectedMapMosViewModel { get; set; } = new([]);
	public MapIconsViewModel SelectedMapIconsViewModel { get; set; } = new([]);
	public MapShopViewModel SelectedMapShopViewModel { get; set; } = new(new());
	public MapDialogueViewModel SelectedMapDialogueViewModel { get; set; } = new([]);

	public static MapsViewModel Instance { get; set; } = new();

	public MapsViewModel()
	{
		SelectedMapIndex = 0;
		Maps.Instance.CollectionRefreshed += MapsCollectionRefreshed;
	}

	private void MapsCollectionRefreshed(object? sender, EventArgs _) =>
		SelectedMapIndex = SelectedMapIndex;

	public bool HasMapDialogue => SelectedMapItem?.Dialogue?.DialogueSectionOpcodes is not null;
	public bool HasMapEnemyGroups => (SelectedMapItem?.EnemyGroups?.Count ?? 0) > 0;
	public bool HasMapEnemyPositions => (SelectedMapItem?.EnemyPositions?.Count ?? 0) > 0;
	public bool HasMapEntries => (SelectedMapItem?.Entries?.Count ?? 0) > 0;
	public bool HasMapHTAs => (SelectedMapItem?.HTAs?.Count ?? 0) > 0;
	public bool HasMapIcons => (SelectedMapItem?.Icons?.Count ?? 0) > 0;
	public bool HasMapInstances => (SelectedMapItem?.Instances?.Count ?? 0) > 0;
	public bool HasMapMOSs => (SelectedMapItem?.MOSs?.Count ?? 0) > 0;
	public bool HasMapScripts => (SelectedMapItem?.Scripts?.Count ?? 0) > 0;
	public bool HasMapShop => SelectedMapItem?.Shop is not null;


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
			SelectedMapEntriesViewModel = new(SelectedMapItem.Entries);
			SelectedMapInstancesViewModel = new(SelectedMapItem.Instances);
			SelectedMapHtaViewModel = new(SelectedMapItem.HTAs);
			SelectedMapScriptsViewModel = new(SelectedMapItem.Scripts);
			SelectedMapEnemyPositionsViewModel = new(SelectedMapItem.EnemyPositions);
			SelectedMapEnemyGroupsViewModel = new(SelectedMapItem.EnemyGroups);
			SelectedMapMosViewModel = new(SelectedMapItem.MOSs);
			SelectedMapIconsViewModel = new(SelectedMapItem.Icons);
			SelectedMapShopViewModel = new(SelectedMapItem.Shop);
			SelectedMapDialogueViewModel = new(SelectedMapItem.Dialogue.DialogueSectionOpcodes);
			OnPropertyChanged(nameof(SelectedMapIndex));
			OnPropertyChanged(nameof(SelectedMapItem));
            OnPropertyChanged(nameof(SelectedMapEntriesViewModel));
            OnPropertyChanged(nameof(SelectedMapInstancesViewModel));
            OnPropertyChanged(nameof(SelectedMapHtaViewModel));
            OnPropertyChanged(nameof(SelectedMapScriptsViewModel));
            OnPropertyChanged(nameof(SelectedMapEnemyPositionsViewModel));
            OnPropertyChanged(nameof(SelectedMapEnemyGroupsViewModel));
            OnPropertyChanged(nameof(SelectedMapMosViewModel));
            OnPropertyChanged(nameof(SelectedMapIconsViewModel));
            OnPropertyChanged(nameof(SelectedMapShopViewModel));
            OnPropertyChanged(nameof(SelectedMapDialogueViewModel));
        }
	}
}
