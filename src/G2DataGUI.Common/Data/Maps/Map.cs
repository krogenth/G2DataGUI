using System.Collections.Generic;
using System.IO;
using System.Text;

namespace G2DataGUI.Common.Data.Maps;

public class Map
{
    public MapHeader Header { get; set; } = new();
	public List<MapEntry> Entries { get; set; } = new();
    public List<MapInstance> Instances { get; set; } = new();
    public List<MapHTA> HTAs { get; set; } = new();
	public List<MapScript> Scripts { get; set; } = new();
    public List<MapEnemyPosition> EnemyPositions { get; set; } = new();
    public List<MapEnemyGroup> EnemyGroups { get; set; } = new();
    public List<MapMOS> MOSs { get; set; } = new();
	public MapDialogue Dialogue { get; set; } = new();
    public List<MapIcon> Icons { get; set; } = new();
	public MapShop Shop { get; set; } = null;

    public string MapName { get; set; } = "";
    public string FileLocation { get; set; } = "";
	public string Filename { get; set; } = "";

    public static Map ReadMap(FileStream reader, string filepath)
    {
		Map map = new()
		{
			Header = MapHeader.ReadMapHeader(reader),
			FileLocation = filepath,
			Filename = Path.GetFileNameWithoutExtension(filepath),
        };

		reader.Seek(map.Header.OffsetMapEntries, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumMapEntries; index++)
		{
			map.Entries.Add(MapEntry.ReadMapEntry(reader));
		}

		reader.Seek(map.Header.OffsetInstances, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumInstances; index++)
		{
			map.Instances.Add(MapInstance.ReadMapInstance(reader));
		}

		reader.Seek(map.Header.OffsetHTA, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumHTA; index++)
		{
			map.HTAs.Add(MapHTA.ReadMapHTA(reader));
		}

		reader.Seek(map.Header.OffsetScripts, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumScripts; index++)
		{
			map.Scripts.Add(MapScript.ReadMapScript(reader, index));
		}

		reader.Seek(map.Header.OffsetEnemyPos, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumEnemyPos; index++)
		{
			map.EnemyPositions.Add(MapEnemyPosition.ReadMapEnemyPosition(reader));
		}

		reader.Seek(map.Header.OffsetEnemyGroups, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumEnemyGroups; index++)
		{
			map.EnemyGroups.Add(MapEnemyGroup.ReadMapEnemyGroup(reader));
		}

		reader.Seek(map.Header.OffsetMOS, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumMOS; index++)
		{
			map.MOSs.Add(MapMOS.ReadMapMOS(reader));
		}

		reader.Seek(map.Header.OffsetDialogue, SeekOrigin.Begin);
		if (map.Header.DialogueLength > 0)
		{
			map.Dialogue = MapDialogue.ReadMapDialogue(reader, map.Header.DialogueLength);
			map.ReadMapName();
		}

		reader.Seek(map.Header.OffsetIcons, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumIcons; index++)
		{
			map.Icons.Add(MapIcon.ReadMapIcon(reader));
		}

		reader.Seek(map.Header.OffsetShop, SeekOrigin.Begin);
		if (map.Header.ShopLength > 0)
		{
			map.Shop = MapShop.ReadMapShop(reader);
		}

        return map;
    }

	public void WriteMap()
	{
		// we need the file to write to
		if (FileLocation.Length <= 0)
		{
			return;
		}

		using FileStream writer = File.Open(FileLocation, FileMode.OpenOrCreate, FileAccess.ReadWrite);

		writer.Seek(Header.OffsetMapEntries, SeekOrigin.Begin);
		foreach (var entry in Entries)
		{
			entry.WriteMapEntry(writer);
		}

		writer.Seek(Header.OffsetInstances, SeekOrigin.Begin);
		foreach (var instance in Instances)
		{
			instance.WriteMapInstance(writer);
		}

		writer.Seek(Header.OffsetHTA, SeekOrigin.Begin);
		foreach (var hta in HTAs)
		{
			hta.WriteMapHTA(writer);
		}

		writer.Seek(Header.OffsetScripts, SeekOrigin.Begin);
		foreach (var script in Scripts)
		{
			script.WriteMapScript(writer);
		}

		writer.Seek(Header.OffsetEnemyPos, SeekOrigin.Begin);
		foreach (var position in EnemyPositions)
		{
			position.WriteMapEnemyPosition(writer);
		}

		writer.Seek(Header.OffsetEnemyGroups, SeekOrigin.Begin);
		foreach (var group in EnemyGroups)
		{
			group.WriteMapEnemyGroup(writer);
		}

		writer.Seek(Header.OffsetMOS, SeekOrigin.Begin);
		foreach (var mos in MOSs)
		{
			mos.WriteMapMOS(writer);
		}

		writer.Seek(Header.OffsetIcons, SeekOrigin.Begin);
		foreach (var icon in Icons)
		{
			icon.WriteMapIcon(writer);
		}

		writer.Seek(Header.OffsetShop, SeekOrigin.Begin);
		if (Shop != null)
		{
			Shop.WriteMapShop(writer);
		}
	}

	/// <summary>
	/// Reads from Dialogue section of a map's mdt file.
	/// This assumes that the dialogue section is not empty
	/// </summary>
	/// <param name="reader"></param>
	private void ReadMapName()
	{
		foreach (var section in Dialogue.DialogueSections)
		{
			if (section.Length <= 0)
			{
				continue;
			}

			if (section[0] != 0x17 || section[1] != 0x80)
			{
				continue;
			}

			List<byte> bytes = new();
			for (var index = 4; index < section.Length && section[index] != 0x17; index++)
			{
				bytes.Add(section[index]);
			}

			var text = Encoding.UTF8.GetString(bytes.ToArray());
			if (!text.Contains("to "))
			{
				MapName = text;
				break;
			}
		}
	}
}
