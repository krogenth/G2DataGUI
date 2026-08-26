using System.Collections.ObjectModel;
using G2DataGUI.Domain.Specials;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Specials;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Moves;

namespace G2DataGUI.UI.ViewModels.Specials;

public sealed class SpecialsViewModel(SpecialSetRepository repository, ILocalizationService locale, ObservableCollection<MoveViewModel> availableMoves)
	: CollectionViewModel<SpecialSet, SpecialSetViewModel>(
		repository.Items,
		model => new SpecialSetViewModel(model, repository, locale, availableMoves));
