using System.Collections.ObjectModel;
using G2DataGUI.Domain.Starting;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Starting;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;
using G2DataGUI.UI.ViewModels.Manaeggs;

namespace G2DataGUI.UI.ViewModels.Starting;

public sealed class InitialViewModel(
	CharacterStartingStatsRepository repository,
	ILocalizationService locale,
	ObservableCollection<GameItemViewModel> availableItems,
	ObservableCollection<ManaeggViewModel> availableManaeggs)
	: CollectionViewModel<CharacterStartingStats, CharacterStartingStatsViewModel>(
		repository.Items,
		model => new CharacterStartingStatsViewModel(model, repository, locale, availableItems, availableManaeggs));
