using System.Collections.Generic;
using System.Linq;
using G2DataGUI.Domain.Levelups;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Levelups;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Levelups;

/// <summary>Levels is fixed-size (99 per character) — projected once at
/// construction. CharacterName is read-only: there's no on-disk name, only
/// the resolved LocaleKeys.Characters[Id] lookup the exporter also uses —
/// see CharacterLevelups.Id's doc comment.</summary>
public sealed class CharacterLevelupsViewModel(CharacterLevelups model, LevelupsRepository repository, ILocalizationService locale) : ItemViewModel<CharacterLevelups>(model, repository)
{
	public int Id => Model.Id;

	public string CharacterName { get; } = locale[LocaleKeys.Characters][model.Id];

	public IReadOnlyList<LevelupStatsViewModel> Levels { get; } = model.Levels.Select(level => new LevelupStatsViewModel(level, repository)).ToList();
}
