using G2DataGUI.Common;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.Repositories.Bosses;
using G2DataGUI.Repositories.Enemies;

namespace G2DataGUI.Contexts;

/// <summary>
/// Everything that forks by game version: enemy/enemy_hardmode and
/// boss/boss_hardmode. One instance per GameVersion, created lazily by
/// GameProjectContext the first time that version is switched to.
///
/// Takes only a VersionGamePaths, not a separate GameVersion — paths.Version
/// is already authoritative, so a second parameter here would just be
/// another pair of values (this.Version, paths.Version) that could disagree
/// with each other, the same problem VersionGamePaths itself was just fixed
/// to not have.
/// </summary>
public sealed class GameVersionContext : AggregateDataContext<IRepository>
{
	public GameVersion Version => Paths.Version;
	public VersionGamePaths Paths { get; }
	public EnemyRepository Enemies { get; }
	public BossRepository Bosses { get; }

	public GameVersionContext(VersionGamePaths paths)
	{
		Paths = paths;
		Enemies = new EnemyRepository(paths.EnemyDirectory);
		Bosses = new BossRepository(paths.BossDirectory);
	}

	protected override IEnumerable<IRepository> Children => [Enemies, Bosses];
}
