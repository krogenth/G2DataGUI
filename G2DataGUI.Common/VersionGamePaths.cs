namespace G2DataGUI.Common;

/// <summary>
/// Paths for a version-specific view of the project root: enemy/boss only,
/// under whichever directory names that GameVersion uses. See GamePaths for
/// the global view (xls_data/map).
///
/// Takes a GameVersion, not folder names — the version→directory-name
/// mapping lives here, as the one place it's allowed to exist, instead of
/// being something a caller assembles. That closes the gap the earlier
/// (root, enemyDirectoryName, bossDirectoryName) constructor still left
/// open: nothing could stop `new VersionGamePaths(root, "enemy",
/// "boss_hardmode")` — two directory names from different versions, paired
/// by nothing but the caller getting it right. With GameVersion as the only
/// input, EnemyDirectory and BossDirectory always come from the same
/// version; there's no pair of strings to mismatch.
/// </summary>
public sealed class VersionGamePaths
{
	private const string EnemyNormalDirectory = "enemy";
	private const string BossNormalDirectory = "boss";
	private const string EnemyHardDirectory = "enemy_hardmode";
	private const string BossHardDirectory = "boss_hardmode";

	public VersionGamePaths(string root, GameVersion version)
	{
		Root = root;
		Version = version;

		(EnemyDirectory, BossDirectory) = version switch
		{
			GameVersion.Normal => (EnemyNormalDirectory, BossNormalDirectory),
			GameVersion.Hard => (EnemyHardDirectory, BossHardDirectory),
			_ => throw new ArgumentOutOfRangeException(nameof(version)),
		};
	}

	public string Root { get; }

	public GameVersion Version { get; }

	public string EnemyDirectory => Path.Combine(Root, field);
	public string BossDirectory => Path.Combine(Root, field);
}
