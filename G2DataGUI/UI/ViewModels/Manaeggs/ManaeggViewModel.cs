using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Domain.Moves;
using G2DataGUI.Repositories.Moves;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Moves;
using G2DataGUI.UI.ViewModels.Manaeggs;

namespace G2DataGUI.UI.ViewModels.Manaeggs;

/// <summary>
/// Spells is a fixed-size list (Manaegg.SpellCount = 18, never added/
/// removed at runtime) — projected once at construction, unlike
/// CollectionViewModel which tracks a growable collection. Pages groups
/// that same list into the 3-pages-of-6 shape the in-game UI actually
/// uses — see ManaeggSpellPageViewModel's own doc comment.
///
/// A real constructor body, not a primary-constructor property
/// initializer, for the same reason GlobalDataViewModel is — Pages'
/// value depends on Spells (SpellsPerPage below), and a property/field
/// INITIALIZER can never reference another instance member of the same
/// object, even one assigned earlier; only a constructor BODY can do
/// that, since by the time body statements run, earlier assignments in
/// that same body are genuinely available.
/// </summary>
public sealed class ManaeggViewModel : ItemViewModel<Manaegg>
{
	private const int SpellsPerPage = 6;

	public ManaeggViewModel(Manaegg model, ManaeggRepository repository, ObservableCollection<MoveViewModel> availableMoves) : base(model, repository)
	{
		Spells = [.. model.Spells.Select((spell, index) => new ManaeggSpellViewModel(spell, repository, availableMoves, index + 1))];

		Pages = [.. Spells.Chunk(SpellsPerPage).Select((pageSpells, index) => new ManaeggSpellPageViewModel(index + 1, pageSpells))];
	}

	public int Id => Model.Id;

	public IReadOnlyList<ManaeggSpellViewModel> Spells { get; }
	public IReadOnlyList<ManaeggSpellPageViewModel> Pages { get; }
}
