using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

public sealed class MapShopSectionViewModel : ItemViewModel<MapShopSection>
{
	public MapShopSectionViewModel(MapShopSection model, IRepository repository, ObservableCollection<GameItemViewModel> availableItems)
		: base(model, repository)
	{
		Slots = Enumerable.Range(0, model.Items.ItemOffsets.Length)
			.Select(i => new MapShopItemSlotViewModel(model.Items, i, repository, availableItems))
			.ToList();
	}

	public string Title
	{
		get => Model.Title;
		set => SetModelValue(value, m => m.Title, (m, v) => m.Title = v);
	}

	public IReadOnlyList<MapShopItemSlotViewModel> Slots { get; }
}
