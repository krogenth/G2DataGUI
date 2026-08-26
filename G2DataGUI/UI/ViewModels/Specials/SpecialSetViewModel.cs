using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Domain.Specials;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Specials;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Moves;

namespace G2DataGUI.UI.ViewModels.Specials;

/// <summary>Moves is fixed-size (SpecialSet.MovesPerSet = 6) — already
/// exactly one page's worth, same as SkillbookViewModel.Skills, so no
/// Pages/chunking here either. CharacterName is read-only, resolved via
/// LocaleKeys.SpecialChars[Id] — same convention as
/// CharacterLevelupsViewModel.CharacterName.</summary>
public sealed class SpecialSetViewModel(SpecialSet model, SpecialSetRepository repository, ILocalizationService locale, ObservableCollection<MoveViewModel> availableMoves)
	: ItemViewModel<SpecialSet>(model, repository)
{
	public int Id => Model.Id;

	public string CharacterName { get; } = locale[LocaleKeys.SpecialChars][model.Id];

	public IReadOnlyList<SpecialMoveViewModel> Moves { get; } =
		model.Moves.Select(move => new SpecialMoveViewModel(move, repository, availableMoves)).ToList();
}
