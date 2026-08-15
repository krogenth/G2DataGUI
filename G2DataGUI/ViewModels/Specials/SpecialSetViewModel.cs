using System.Collections.Generic;
using System.Linq;
using G2DataGUI.Domain.Specials;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Specials;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Specials;

/// <summary>Moves is fixed-size (SpecialSet.MovesPerSet = 6). CharacterName
/// is read-only, resolved via LocaleKeys.SpecialChars[Id] — same convention
/// as CharacterLevelupsViewModel.CharacterName.</summary>
public sealed class SpecialSetViewModel(SpecialSet model, SpecialSetRepository repository, ILocalizationService locale) : ItemViewModel<SpecialSet>(model, repository)
{
	public int Id => Model.Id;

	public string CharacterName { get; } = locale[LocaleKeys.SpecialChars][model.Id];

	public IReadOnlyList<SpecialMoveViewModel> Moves { get; } = model.Moves.Select(move => new SpecialMoveViewModel(move, repository)).ToList();
}
