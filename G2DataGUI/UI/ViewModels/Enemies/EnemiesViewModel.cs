using System.Collections.ObjectModel;
using G2DataGUI.Domain.Enemies;
using G2DataGUI.Repositories.Enemies;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;

namespace G2DataGUI.UI.ViewModels.Enemies;

public sealed class EnemiesViewModel(EnemyRepository repository, ObservableCollection<GameItemViewModel> availableItems)
	: CollectionViewModel<Enemy, EnemyViewModel>(repository.Items, model => new EnemyViewModel(model, repository, availableItems));
