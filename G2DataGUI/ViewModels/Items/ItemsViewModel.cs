using G2DataGUI.Domain.Items;
using G2DataGUI.Repositories.Items;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Items;

public sealed class ItemsViewModel(ItemRepository repository)
	: CollectionViewModel<Item, GameItemViewModel>(repository.Items, model => new GameItemViewModel(model, repository));
