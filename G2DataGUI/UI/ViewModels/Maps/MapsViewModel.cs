using System.Collections.ObjectModel;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Maps;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;

namespace G2DataGUI.UI.ViewModels.Maps;

/// <summary>
/// availableMaps is passed in from GlobalDataViewModel already-empty, not
/// created here — Maps reference OTHER maps (Entries.Id, Scripts.MapID —
/// see MapEntryViewModel/MapScriptViewModel), so unlike every other
/// "reference the live collection" case in this project (Manaeggs
/// referencing Moves, Skillbooks referencing Skills), there's no
/// already-built different collection to hand each MapViewModel at
/// construction time — Items IS that collection, and it doesn't exist
/// yet until the base CollectionViewModel constructor below finishes.
///
/// Syncing availableMaps is NOT a one-time copy after base() — MapRepository
/// starts empty and is only actually populated later, asynchronously, by
/// MapRepository.LoadAsync (this constructor runs at object-graph-build
/// time, well before that). CollectionViewModel's own Items updates
/// correctly once loading happens (it's already wired to react to the
/// underlying repository's CollectionChanged), so this subscribes to
/// Items.CollectionChanged itself and re-syncs availableMaps every time
/// Items changes — at initial load, and on any future reload — rather
/// than copying it once when there's nothing yet to copy.
/// </summary>
public sealed class MapsViewModel : CollectionViewModel<Map, MapViewModel>
{
	public MapsViewModel(
		MapRepository repository,
		ObservableCollection<GameItemViewModel> availableItems,
		ObservableCollection<MapViewModel> availableMaps)
		: base(repository.Items, model => new MapViewModel(model, repository, availableItems, availableMaps))
	{
		SyncAvailableMaps(availableMaps);

		Items.CollectionChanged += (_, _) => SyncAvailableMaps(availableMaps);
	}

	private void SyncAvailableMaps(ObservableCollection<MapViewModel> availableMaps)
	{
		availableMaps.Clear();

		foreach (MapViewModel map in Items)
		{
			availableMaps.Add(map);
		}
	}
}
