using G2DataGUI.Domain.Specials;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Specials;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Specials;

public sealed class SpecialsViewModel(SpecialSetRepository repository, ILocalizationService locale)
	: CollectionViewModel<SpecialSet, SpecialSetViewModel>(
		repository.Items,
		model => new SpecialSetViewModel(model, repository, locale));
