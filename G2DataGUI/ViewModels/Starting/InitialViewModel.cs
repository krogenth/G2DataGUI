using G2DataGUI.Domain.Starting;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Starting;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Starting;

public sealed class InitialViewModel(CharacterStartingStatsRepository repository, ILocalizationService locale)
	: CollectionViewModel<CharacterStartingStats, CharacterStartingStatsViewModel>(
		repository.Items,
		model => new CharacterStartingStatsViewModel(model, repository, locale));
