using System.Collections.ObjectModel;
using G2DataGUI.Domain.Bosses;
using G2DataGUI.Repositories.Bosses;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;

namespace G2DataGUI.UI.ViewModels.Bosses;

public sealed class BossesViewModel(BossRepository repository, ObservableCollection<GameItemViewModel> availableItems)
	: CollectionViewModel<Boss, BossViewModel>(repository.Items, model => new BossViewModel(model, repository, availableItems));
