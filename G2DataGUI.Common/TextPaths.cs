namespace G2DataGUI.Common;

/// <summary>
/// Paths for one language's translation files: root/content/data/text/[language]/scnr/,
/// one [map].scnr file per map — each holding that map's dialogue section
/// (the same byte format the .mdt's own embedded dialogue uses, just as a
/// standalone file, one per language, for translation).
///
/// Takes root (the project root — same one GameProjectPaths.Root is) and a
/// language code (e.g. "en", "de") directly, rather than being handed an
/// already-resolved directory — same reasoning as VersionGamePaths taking
/// a GameVersion instead of directory-name strings: there's nothing here
/// that could point language and directory at a mismatched pair, because
/// there's only ever the one derivation.
/// </summary>
public sealed class TextPaths(string root, string language)
{
	private const string ContentDirectory = "content";
	private const string DataDirectory = "data";
	private const string TextDirectory = "text";
	private const string ScnrDirectory = "scnr";

	public string Root { get; } = root;

	public string Language { get; } = language;

	private string ScnrRoot =>
		Path.Combine(Root, ContentDirectory, DataDirectory, TextDirectory, Language, ScnrDirectory);

	public string Scnr(string mapName) =>
		Path.Combine(ScnrRoot, mapName + ".scnr");
}
