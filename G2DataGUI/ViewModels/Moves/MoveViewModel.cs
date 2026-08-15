using G2DataGUI.Domain.Moves;
using G2DataGUI.Repositories.Moves;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Moves;

/// <summary>
/// The template for a single-item ViewModel: one property per model field,
/// each a two-line get/set pair around SetModelValue. Id is read-only in
/// the UI on purpose — it's position-in-file, not something the user
/// should be able to edit (see Move.Id's doc comment).
/// </summary>
public sealed class MoveViewModel : ItemViewModel<Move>
{
	public MoveViewModel(Move model, MoveRepository repository) : base(model, repository)
	{
		Stats = new MoveStatsViewModel(model.Stats, repository);
	}

	public int Id => Model.Id;

	public byte Icon
	{
		get => Model.Icon;
		set => SetModelValue(value, m => m.Icon, (m, v) => m.Icon = v);
	}

	public string Name
	{
		get => Model.Name;
		set => SetModelValue(value, m => m.Name, (m, v) => m.Name = v);
	}

	public MoveStatsViewModel Stats { get; }

	public string Description
	{
		get => Model.Description;
		set => SetModelValue(value, m => m.Description, (m, v) => m.Description = v);
	}
}
