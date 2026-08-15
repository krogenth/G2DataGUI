using G2DataGUI.Common;
using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Contexts;

/// <summary>
/// Root of the context tree: one GlobalDataContext, plus a GameVersionContext
/// per version that's actually been switched to (built lazily — the game
/// usually only needs one version loaded at a time).
/// </summary>
public sealed class GameProjectContext : AggregateDataContext<IDataContext>
{
	private readonly Dictionary<GameVersion, GameVersionContext> _versions = [];

	public GameProjectPaths Paths { get; }
	public GlobalDataContext Global { get; }
	public GameVersion CurrentVersion { get; private set; }
	public GameVersionContext Current => _versions[CurrentVersion];
	public IReadOnlyDictionary<GameVersion, GameVersionContext> Versions => _versions;

	public GameProjectContext(GameProjectPaths paths, GameVersion initialVersion)
	{
		Paths = paths;
		CurrentVersion = initialVersion;
		Global = new GlobalDataContext(paths.Global);
	}

	protected override IEnumerable<IDataContext> Children =>
		new IDataContext[] { Global }.Concat(_versions.Values);

	public async Task SwitchVersionAsync(
		GameVersion version,
		CancellationToken cancellationToken = default)
	{
		if (!_versions.TryGetValue(version, out GameVersionContext? context))
		{
			context = new GameVersionContext(Paths.ForVersion(version));
			_versions.Add(version, context);

			await context.LoadAsync(cancellationToken).ConfigureAwait(false);
		}

		CurrentVersion = version;
	}

	public override async Task LoadAsync(CancellationToken cancellationToken = default)
	{
		// base.LoadAsync() walks Global + whatever's already in _versions
		// (nothing yet, on first load) — SwitchVersionAsync below ensures
		// CurrentVersion actually gets loaded too.
		await base.LoadAsync(cancellationToken).ConfigureAwait(false);

		await SwitchVersionAsync(CurrentVersion, cancellationToken).ConfigureAwait(false);
	}
}
