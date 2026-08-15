namespace G2DataGUI.Common;

/// <summary>
/// Entry point for path resolution: one project root, two different views
/// carved out of it depending on whether you need the shared data
/// (GamePaths) or a specific game version's data (VersionGamePaths).
/// </summary>
public sealed class GameProjectPaths(string root)
{
	public string Root { get; } = root;

	public GamePaths Global { get; } = new(root);

	public VersionGamePaths ForVersion(GameVersion version) => new(Root, version);
}
