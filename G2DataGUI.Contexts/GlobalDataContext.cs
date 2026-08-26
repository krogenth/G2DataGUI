using G2DataGUI.Common;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.Repositories.Items;
using G2DataGUI.Repositories.Levelups;
using G2DataGUI.Repositories.Maps;
using G2DataGUI.Repositories.Moves;
using G2DataGUI.Repositories.Skills;
using G2DataGUI.Repositories.Specials;
using G2DataGUI.Repositories.Starting;

namespace G2DataGUI.Contexts;

/// <summary>
/// Everything under xls_data/ and map/ — shared between Normal and Hard,
/// loaded once per project regardless of which version is active.
/// </summary>
public sealed class GlobalDataContext : AggregateDataContext<IRepository>
{
	public MoveRepository Moves { get; }
	public SkillRepository Skills { get; }
	public ManaeggRepository Manaeggs { get; }
	public SkillbookRepository Skillbooks { get; }
	public LevelupsRepository Levelups { get; }
	public ItemRepository Items { get; }
	public SpecialSetRepository Specials { get; }
	public CharacterStartingStatsRepository Initial { get; }
	public MapRepository Maps { get; }

	public GlobalDataContext(GamePaths paths)
	{
		Moves = new MoveRepository(paths.Moves);
		Skills = new SkillRepository(paths.Skills);
		Manaeggs = new ManaeggRepository(paths.Manaeggs);
		Skillbooks = new SkillbookRepository(paths.Skillbooks);
		Levelups = new LevelupsRepository(paths.Levelups);
		Items = new ItemRepository(paths.Items);
		Specials = new SpecialSetRepository(paths.Specials);
		Initial = new CharacterStartingStatsRepository(paths.Initial);
		Maps = new MapRepository(paths.Map);
	}

	protected override IEnumerable<IRepository> Children =>
		[Moves, Skills, Manaeggs, Skillbooks, Levelups, Items, Specials, Initial, Maps];
}
