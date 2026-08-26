using G2DataGUI.Domain.Maps;

namespace G2DataGUI.Serialization.Maps;

/// <summary>
/// Writes a Map's sections back to its .mdt file, in place. Ported
/// behavior, worth being explicit about: this does NOT rewrite
/// MapHeader itself, and does NOT write the Dialogue section at all — it
/// assumes every section's count and offset are unchanged from what was
/// read (matching that nothing in this pass supports adding/removing
/// entries, only editing existing ones), and Dialogue is read-only (see
/// MapDialogueReader's own doc comment). This is an in-place edit of
/// existing fixed-size records at their existing offsets, not a
/// restructure of the file.
/// </summary>
public static class MapWriter
{
	public static void Write(Stream stream, Map map)
	{
		stream.Seek(map.Header.OffsetMapEntries, SeekOrigin.Begin);
		foreach (MapEntry entry in map.Entries)
		{
			MapRecordReaders.WriteMapEntry(stream, entry);
		}

		stream.Seek(map.Header.OffsetInstances, SeekOrigin.Begin);
		foreach (MapInstance instance in map.Instances)
		{
			MapRecordReaders.WriteMapInstance(stream, instance);
		}

		stream.Seek(map.Header.OffsetHTA, SeekOrigin.Begin);
		foreach (MapHTA hta in map.HTAs)
		{
			MapRecordReaders.WriteMapHTA(stream, hta);
		}

		stream.Seek(map.Header.OffsetScripts, SeekOrigin.Begin);
		foreach (MapScript script in map.Scripts)
		{
			MapRecordReaders.WriteMapScript(stream, script);
		}

		stream.Seek(map.Header.OffsetEnemyPos, SeekOrigin.Begin);
		foreach (MapEnemyPosition position in map.EnemyPositions)
		{
			MapRecordReaders.WriteMapEnemyPosition(stream, position);
		}

		stream.Seek(map.Header.OffsetEnemyGroups, SeekOrigin.Begin);
		foreach (MapEnemyGroup group in map.EnemyGroups)
		{
			MapEnemyGroupReader.Write(stream, group);
		}

		stream.Seek(map.Header.OffsetMOS, SeekOrigin.Begin);
		foreach (MapMOS mos in map.MOSs)
		{
			MapRecordReaders.WriteMapMOS(stream, mos);
		}

		stream.Seek(map.Header.OffsetIcons, SeekOrigin.Begin);
		foreach (MapIcon icon in map.Icons)
		{
			MapRecordReaders.WriteMapIcon(stream, icon);
		}

		if (map.Shop is not null)
		{
			stream.Seek(map.Header.OffsetShop, SeekOrigin.Begin);
			MapShopReader.Write(stream, map.Shop);
		}
	}
}
