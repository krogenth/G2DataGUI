using G2DataGUI.Common;
using G2DataGUI.Contexts;
using G2DataGUI.ViewModels.Bosses;
using G2DataGUI.ViewModels.Enemies;

namespace G2DataGUI.ViewModels;

/// <summary>
/// Wraps one GameVersionContext — enemy/boss data for exactly one
/// GameVersion. One of these exists per version that's actually been
/// switched to, cached by GameProjectViewModel the same way
/// GameProjectContext caches the underlying GameVersionContexts, so
/// switching back to a version you've already visited doesn't rebuild its
/// ViewModels from scratch.
/// </summary>
public sealed class GameVersionViewModel
{
	public GameVersionViewModel(GameVersionContext context)
	{
		Version = context.Version;
		Enemies = new EnemiesViewModel(context.Enemies);
		Bosses = new BossesViewModel(context.Bosses);
	}

	public GameVersion Version { get; }

	public EnemiesViewModel Enemies { get; }

	public BossesViewModel Bosses { get; }
}
