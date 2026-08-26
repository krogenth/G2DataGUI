namespace G2DataGUI.Domain.Maps;

/// <summary>
/// The dialogue section, both framed AND decoded — DialogueSections holds
/// each section's raw bytes (split up using MapDialogueHeader's offset
/// table); Entries is the same sections decoded into actual opcode
/// sequences via MapDialogueSectionReader, which reuses the existing
/// MapOpcodeReader (in place since early in this project — see
/// PORTING.md) rather than anything new. Both are kept: DialogueSections
/// because MapReader.ExtractMapName scans it directly (predates and
/// doesn't need the decode), and because a section that fails to decode
/// (see MapDialogueEntry.DecodeError) still has its raw bytes available.
///
/// The first header offset entry is intentionally NOT turned into a
/// section (MapReader's loop starts at index 1) — ported behavior, not an
/// oversight; index 0 appears to be a sentinel/base reference rather than
/// a real section, matching the original implementation exactly.
/// </summary>
public sealed class MapDialogue
{
	public MapDialogueHeader Header { get; set; } = new();
	public List<byte[]> DialogueSections { get; set; } = [];
	public List<MapDialogueEntry> Entries { get; set; } = [];
}

public sealed class MapDialogueHeader
{
	public uint HeaderLength { get; set; }
	public List<MapDialogueOffset> Offsets { get; set; } = [];
}

public sealed class MapDialogueOffset
{
	public ushort Unknown1 { get; set; }
	public ushort Offset { get; set; }
}

/// <summary>
/// One dialogue section, decoded. DecodeError is non-null when
/// MapDialogueSectionReader hit something it couldn't parse (an
/// unrecognized opcode byte, a malformed nested run — see
/// TextOpcodeReader's own doc comment on why a malformed stream now
/// throws rather than silently truncating) — Opcodes holds whatever WAS
/// successfully decoded before that happened, not necessarily empty.
/// One bad section doesn't stop the rest of the map's dialogue (or the
/// rest of the map itself) from loading.
/// </summary>
public sealed class MapDialogueEntry
{
	public int Index { get; set; }
	public byte[] RawBytes { get; set; } = [];
	public List<IMapOpcode> Opcodes { get; set; } = [];
	public string? DecodeError { get; set; }
}
