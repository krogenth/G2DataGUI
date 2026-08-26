namespace G2DataGUI.Common;

/// <summary>
/// Entry point for path resolution: one project root, three different
/// views carved out of it — the shared game data (Global), a specific
/// game version's data (ForVersion), and a specific language's
/// translation files (ForLanguage).
///
/// Root is the folder the user actually picks (File > Open Project) — the
/// true top-level directory, NOT root/content/data/afs/, which is what
/// this used to treat as "root" before the real structure was known:
///
///   root/
///   ├── content/data/afs/    ← xls_data/, map/, enemy/, boss/, ... (what
///   │                          GamePaths/VersionGamePaths need — resolved
///   │                          here, once, so those two types never had
///   │                          to change at all)
///   └── content/data/text/[language]/scnr/[map].scnr  (translations —
///                                                        see TextPaths)
///
/// AfsRoot is computed once (a static helper, not a property — see the
/// note on why below) and used by both Global's initializer and
/// ForVersion, so the content/data/afs segment is written in exactly one
/// place rather than duplicated between them.
/// </summary>
public sealed class GameProjectPaths(string root)
{
	private const string ContentDirectory = "content";
	private const string DataDirectory = "data";
	private const string AfsDirectory = "afs";

	public string Root { get; } = root;

	// A property initializer can't reference another instance member of
	// the same object (see GlobalDataViewModel/ManaeggViewModel for where
	// that bit before) — ResolveAfsRoot is a static method taking root
	// directly (a primary-constructor parameter, not an instance member),
	// so both Global's initializer and ForVersion below can call it
	// without hitting that restriction.
	public GamePaths Global { get; } = new(ResolveAfsRoot(root));

	public VersionGamePaths ForVersion(GameVersion version) =>
		new(ResolveAfsRoot(Root), version);

	public TextPaths ForLanguage(string language) =>
		new(Root, language);

	private static string ResolveAfsRoot(string root) =>
		Path.Combine(root, ContentDirectory, DataDirectory, AfsDirectory);
}
