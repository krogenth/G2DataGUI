using G2DataGUI.Domain.Bosses;
using G2DataGUI.Repositories.Bosses;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Bosses;

public sealed class BossesViewModel(BossRepository repository)
	: CollectionViewModel<Boss, BossViewModel>(repository.Items, model => new BossViewModel(model, repository));
