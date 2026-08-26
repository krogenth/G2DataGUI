using System.Collections.ObjectModel;
using G2DataGUI.Domain.Moves;
using G2DataGUI.Repositories.Moves;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Moves;

namespace G2DataGUI.UI.ViewModels.Manaeggs;

public sealed class ManaeggsViewModel(ManaeggRepository repository, ObservableCollection<MoveViewModel> availableMoves)
	: CollectionViewModel<Manaegg, ManaeggViewModel>(repository.Items, model => new ManaeggViewModel(model, repository, availableMoves));
