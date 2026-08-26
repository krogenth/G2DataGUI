using System.Collections.ObjectModel;
using G2DataGUI.Contexts;
using G2DataGUI.Locale;
using G2DataGUI.UI.ViewModels.Items;
using G2DataGUI.UI.ViewModels.Levelups;
using G2DataGUI.UI.ViewModels.Manaeggs;
using G2DataGUI.UI.ViewModels.Maps;
using G2DataGUI.UI.ViewModels.Moves;
using G2DataGUI.UI.ViewModels.Skillbooks;
using G2DataGUI.UI.ViewModels.Skills;
using G2DataGUI.UI.ViewModels.Specials;
using G2DataGUI.UI.ViewModels.Starting;

namespace G2DataGUI.UI.ViewModels;

/// <summary>
/// Wraps GlobalDataContext: xls_data/ and map/ never fork by version, so
/// unlike GameVersionViewModel below, nothing here ever gets rebuilt or
/// swapped out — one instance for the lifetime of the loaded project.
/// </summary>
public sealed class GlobalDataViewModel
{
	public MovesViewModel Moves { get; }

	public ManaeggsViewModel Manaeggs { get; }

	public SkillsViewModel Skills { get; }

	public SkillbooksViewModel Skillbooks { get; }

	public LevelupsViewModel Levelups { get; }

	public ItemsViewModel Items { get; }

	public SpecialsViewModel Specials { get; }

	public InitialViewModel Initial { get; }

	public MapsViewModel Maps { get; }

	public GlobalDataViewModel(GlobalDataContext context, ILocalizationService locale)
	{
		Moves = new MovesViewModel(context.Moves, locale);
		Manaeggs = new ManaeggsViewModel(context.Manaeggs, Moves.Items);
		Skills = new SkillsViewModel(context.Skills);
		Skillbooks = new SkillbooksViewModel(context.Skillbooks, Skills.Items);
		Levelups = new LevelupsViewModel(context.Levelups, locale);
		Items = new ItemsViewModel(context.Items);
		Specials = new SpecialsViewModel(context.Specials, locale, Moves.Items);
		Initial = new InitialViewModel(context.Initial, locale, Items.Items, Manaeggs.Items);
		// Empty when passed in — Maps' own Items isn't populated yet at
		// this point (MapsViewModel's base CollectionViewModel constructor
		// hasn't run), and there's no way to reference "MapsViewModel's own
		// Items" before it exists anyway (same "can't reference an
		// instance member from a base(...) call" restriction as
		// GlobalDataViewModel needing a real constructor body — this is
		// that same restriction one level deeper, since maps reference
		// OTHER maps, not a different already-built collection like
		// Manaeggs referencing Moves). MapsViewModel fills this in, once,
		// after its own Items is populated — see its own doc comment.
		ObservableCollection<MapViewModel> availableMaps = [];
		Maps = new MapsViewModel(context.Maps, Items.Items, availableMaps);
	}
}
