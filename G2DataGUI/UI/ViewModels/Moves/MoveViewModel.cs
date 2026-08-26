using G2DataGUI.Domain.Moves;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Moves;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Moves;

/// <summary>
/// The template for a single-item ViewModel: one property per model field,
/// each a two-line get/set pair around SetModelValue. Id is read-only in
/// the UI on purpose — it's position-in-file, not something the user
/// should be able to edit (see Move.Id's doc comment).
/// </summary>
public sealed class MoveViewModel(Move model, MoveRepository repository, ILocalizationService locale) : ItemViewModel<Move>(model, repository)
{
	private readonly ILocalizationService _locale = locale;

	public int Id => Model.Id;

	public int MaxNameLength => Move.MaxNameLength;
	public int MaxDescriptionLength => Move.MaxDescriptionLength;

	public string IconName => _locale[LocaleKeys.Icons][Model.Icon];

	public byte Icon
	{
		get => Model.Icon;
		set
		{
			SetModelValue(value, m => m.Icon, (m, v) => m.Icon = v);
			OnPropertyChanged(nameof(IconName));
		}
	}

	public string Name
	{
		get => Model.Name;
		set => SetModelValue(value, m => m.Name, (m, v) => m.Name = v);
	}

	public MoveStatsViewModel Stats { get; } = new MoveStatsViewModel(model.Stats, repository, locale);

	public string Description
	{
		get => Model.Description;
		set => SetModelValue(value, m => m.Description, (m, v) => m.Description = v);
	}
}
