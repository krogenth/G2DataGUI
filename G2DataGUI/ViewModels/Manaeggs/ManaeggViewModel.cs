using System.Collections.Generic;
using System.Linq;
using G2DataGUI.Domain.Moves;
using G2DataGUI.Repositories.Moves;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Manaeggs;

/// <summary>
/// Spells is a fixed-size list (Manaegg.SpellCount = 18, never
/// added/removed at runtime) — projected once at construction, unlike
/// CollectionViewModel which tracks a growable collection.
/// </summary>
public sealed class ManaeggViewModel(Manaegg model, ManaeggRepository repository) : ItemViewModel<Manaegg>(model, repository)
{
	public int Id => Model.Id;

	public IReadOnlyList<ManaeggSpellViewModel> Spells { get; } = model.Spells.Select(spell => new ManaeggSpellViewModel(spell, repository)).ToList();
}
