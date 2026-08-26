using G2DataGUI.Domain.Moves;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Moves;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Moves;

/// <summary>
/// The template for a collection ViewModel: once the item ViewModel exists
/// (MoveViewModel), wrapping the whole repository is this one-liner —
/// CollectionViewModel already handles keeping Items in sync with
/// MoveRepository.Items as the repository loads/reloads.
/// </summary>
public sealed class MovesViewModel(MoveRepository repository, ILocalizationService locale)
	: CollectionViewModel<Move, MoveViewModel>(repository.Items, model => new MoveViewModel(model, repository, locale));
