using System.Collections.ObjectModel;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

/// <summary>Wraps Map.Shop — a MapViewModel constructs this only when
/// Shop is non-null (Header.ShopLength > 0); MapsView shows a "no shop"
/// message for maps without one rather than an empty shop form.</summary>
public sealed class MapShopViewModel : ItemViewModel<MapShop>
{
	public MapShopViewModel(MapShop model, IRepository repository, ObservableCollection<GameItemViewModel> availableItems)
		: base(model, repository)
	{
		Weapons = new MapShopSectionViewModel(model.Weapons, repository, availableItems);
		Armors = new MapShopSectionViewModel(model.Armors, repository, availableItems);
		Jewelry = new MapShopSectionViewModel(model.Jewelry, repository, availableItems);
		Items = new MapShopSectionViewModel(model.Items, repository, availableItems);
		Regionals = new MapShopSectionViewModel(model.Regionals, repository, availableItems);
	}

	public ushort MapId
	{
		get => Model.MapId;
		set => SetModelValue(value, m => m.MapId, (m, v) => m.MapId = v);
	}

	public ushort Categories
	{
		get => Model.Categories;
		set => SetModelValue(value, m => m.Categories, (m, v) => m.Categories = v);
	}

	public MapShopSectionViewModel Weapons { get; }
	public MapShopSectionViewModel Armors { get; }
	public MapShopSectionViewModel Jewelry { get; }
	public MapShopSectionViewModel Items { get; }
	public MapShopSectionViewModel Regionals { get; }
}
