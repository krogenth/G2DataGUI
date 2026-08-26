using System.Collections.ObjectModel;
using G2DataGUI.Common;
using G2DataGUI.Contexts;
using G2DataGUI.UI.ViewModels.Bosses;
using G2DataGUI.UI.ViewModels.Enemies;
using G2DataGUI.UI.ViewModels.Items;

namespace G2DataGUI.UI.ViewModels;

/// <summary>
/// Wraps one GameVersionContext — enemy/boss data for exactly one
/// GameVersion. One of these exists per version that's actually been
/// switched to, cached by GameProjectViewModel the same way
/// GameProjectContext caches the underlying GameVersionContexts, so
/// switching back to a version you've already visited doesn't rebuild its
/// ViewModels from scratch.
///
/// Takes availableItems (Global.Items.Items, from GameProjectViewModel) —
/// Enemy/Boss item drops (Item1Offset/Item2Offset) reference the GLOBAL
/// Items list even though Enemy/Boss themselves are version-specific, so
/// this is the point where that global data crosses into the
/// version-specific ViewModel tree. See EnemyStatsViewModel's own doc
/// comment for where it ends up.
/// </summary>
public sealed class GameVersionViewModel
{
	public GameVersionViewModel(GameVersionContext context, ObservableCollection<GameItemViewModel> availableItems)
	{
		Version = context.Version;
		Enemies = new EnemiesViewModel(context.Enemies, availableItems);
		Bosses = new BossesViewModel(context.Bosses, availableItems);
	}

	public GameVersion Version { get; }

	public EnemiesViewModel Enemies { get; }

	public BossesViewModel Bosses { get; }
}
