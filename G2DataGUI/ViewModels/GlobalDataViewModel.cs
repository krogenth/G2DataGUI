using G2DataGUI.Contexts;
using G2DataGUI.Locale;
using G2DataGUI.ViewModels.Items;
using G2DataGUI.ViewModels.Levelups;
using G2DataGUI.ViewModels.Manaeggs;
using G2DataGUI.ViewModels.Moves;
using G2DataGUI.ViewModels.Skillbooks;
using G2DataGUI.ViewModels.Skills;
using G2DataGUI.ViewModels.Specials;
using G2DataGUI.ViewModels.Starting;

namespace G2DataGUI.ViewModels;

/// <summary>
/// Wraps GlobalDataContext: xls_data/ never forks by version, so unlike
/// GameVersionViewModel below, nothing here ever gets rebuilt or swapped
/// out — one instance for the lifetime of the loaded project.
/// </summary>
public sealed class GlobalDataViewModel
{
	public GlobalDataViewModel(GlobalDataContext context, ILocalizationService locale)
	{
		Moves = new MovesViewModel(context.Moves);
		Manaeggs = new ManaeggsViewModel(context.Manaeggs);
		Skills = new SkillsViewModel(context.Skills);
		Skillbooks = new SkillbooksViewModel(context.Skillbooks);
		Levelups = new LevelupsViewModel(context.Levelups, locale);
		Items = new ItemsViewModel(context.Items);
		Specials = new SpecialsViewModel(context.Specials, locale);
		Initial = new InitialViewModel(context.Initial, locale);
	}

	public MovesViewModel Moves { get; }

	public ManaeggsViewModel Manaeggs { get; }

	public SkillsViewModel Skills { get; }

	public SkillbooksViewModel Skillbooks { get; }

	public LevelupsViewModel Levelups { get; }

	public ItemsViewModel Items { get; }

	public SpecialsViewModel Specials { get; }

	public InitialViewModel Initial { get; }
}
