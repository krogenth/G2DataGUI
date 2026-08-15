using G2DataGUI.Domain.Moves;
using G2DataGUI.Repositories.Moves;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Manaeggs;

public sealed class ManaeggsViewModel(ManaeggRepository repository)
	: CollectionViewModel<Manaegg, ManaeggViewModel>(repository.Items, model => new ManaeggViewModel(model, repository));
