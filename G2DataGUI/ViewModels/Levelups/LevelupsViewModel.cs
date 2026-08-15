using G2DataGUI.Domain.Levelups;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Levelups;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Levelups;

public sealed class LevelupsViewModel(LevelupsRepository repository, ILocalizationService locale)
	: CollectionViewModel<CharacterLevelups, CharacterLevelupsViewModel>(
		repository.Items,
		model => new CharacterLevelupsViewModel(model, repository, locale));
