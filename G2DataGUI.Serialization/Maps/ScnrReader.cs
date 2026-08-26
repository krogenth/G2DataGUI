using G2DataGUI.Domain.Maps;

namespace G2DataGUI.Serialization.Maps;

/// <summary>
/// Reads a standalone .scnr file — one language's translation of a map's
/// dialogue section (root/content/data/text/[language]/scnr/[map].scnr —
/// see TextPaths). Same byte format as the dialogue section embedded in
/// the .mdt itself (MapDialogueHeader + offset-framed sections), just as
/// its own file rather than embedded at an offset inside a larger one —
/// reuses MapDialogueReader.Read directly, passing the whole file's own
/// length as the section length (there's no larger container here to
/// read a length from; the entire file IS the dialogue section).
///
/// This is deliberately just the reader — no repository, ViewModel, or
/// View wraps this yet. Extracting each map's display name already reuses
/// this same dialogue-section shape (MapReader.ExtractMapName), so the
/// pattern for pulling readable text back out of DialogueSections exists,
/// but nothing here builds a translation-editing UI on top of it.
/// </summary>
public static class ScnrReader
{
	public static MapDialogue Read(Stream stream) =>
		MapDialogueReader.Read(stream, (uint)stream.Length);
}
