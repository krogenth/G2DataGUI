using G2DataGUI.Domain.Enemies;
using G2DataGUI.Repositories.Enemies;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Enemies;

public sealed class EnemiesViewModel(EnemyRepository repository)
	: CollectionViewModel<Enemy, EnemyViewModel>(repository.Items, model => new EnemyViewModel(model, repository));
