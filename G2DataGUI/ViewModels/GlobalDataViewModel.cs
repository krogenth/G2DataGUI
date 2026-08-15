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
public sealed class GlobalDataViewModel(GlobalDataContext context, ILocalizationService locale)
{
	public MovesViewModel Moves { get; } = new MovesViewModel(context.Moves, locale);

	public ManaeggsViewModel Manaeggs { get; } = new ManaeggsViewModel(context.Manaeggs);

	public SkillsViewModel Skills { get; } = new SkillsViewModel(context.Skills);

	public SkillbooksViewModel Skillbooks { get; } = new SkillbooksViewModel(context.Skillbooks);

	public LevelupsViewModel Levelups { get; } = new LevelupsViewModel(context.Levelups, locale);

	public ItemsViewModel Items { get; } = new ItemsViewModel(context.Items);

	public SpecialsViewModel Specials { get; } = new SpecialsViewModel(context.Specials, locale);

	public InitialViewModel Initial { get; } = new InitialViewModel(context.Initial, locale);
}
