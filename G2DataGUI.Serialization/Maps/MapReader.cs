using System.Text;
using G2DataGUI.Domain.Maps;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Maps;

/// <summary>
/// Reads a full .mdt file — the container-level framing (MapHeader's
/// counts/offsets, seeking to each section) that ties the individual
/// section readers together into one Map. Every section is addressed by
/// an absolute offset from the header, not by continuing where the
/// previous section left off, so getting one section's exact length
/// wrong (see MapShopReader's Title-length caveat) can't cascade into
/// corrupting an unrelated section's read.
/// </summary>
public static class MapReader
{
	public static Map Read(Stream stream, string name, string mdtPath, string? chrPath, string? texturesPath)
	{
		Map map = new()
		{
			Name = name,
			MdtPath = mdtPath,
			ChrPath = chrPath,
			TexturesPath = texturesPath,
			Header = MapHeaderReader.Read(stream),
		};

		stream.Seek(map.Header.OffsetMapEntries, SeekOrigin.Begin);
		for (int i = 0; i < map.Header.NumMapEntries; i++)
		{
			map.Entries.Add(MapRecordReaders.ReadMapEntry(stream));
		}

		stream.Seek(map.Header.OffsetInstances, SeekOrigin.Begin);
		for (int i = 0; i < map.Header.NumInstances; i++)
		{
			map.Instances.Add(MapRecordReaders.ReadMapInstance(stream));
		}

		stream.Seek(map.Header.OffsetHTA, SeekOrigin.Begin);
		for (int i = 0; i < map.Header.NumHTA; i++)
		{
			map.HTAs.Add(MapRecordReaders.ReadMapHTA(stream));
		}

		stream.Seek(map.Header.OffsetScripts, SeekOrigin.Begin);
		for (int i = 0; i < map.Header.NumScripts; i++)
		{
			map.Scripts.Add(MapRecordReaders.ReadMapScript(stream, i));
		}

		stream.Seek(map.Header.OffsetEnemyPos, SeekOrigin.Begin);
		for (int i = 0; i < map.Header.NumEnemyPos; i++)
		{
			map.EnemyPositions.Add(MapRecordReaders.ReadMapEnemyPosition(stream));
		}

		stream.Seek(map.Header.OffsetEnemyGroups, SeekOrigin.Begin);
		for (int i = 0; i < map.Header.NumEnemyGroups; i++)
		{
			map.EnemyGroups.Add(MapEnemyGroupReader.Read(stream));
		}

		stream.Seek(map.Header.OffsetMOS, SeekOrigin.Begin);
		for (int i = 0; i < map.Header.NumMOS; i++)
		{
			map.MOSs.Add(MapRecordReaders.ReadMapMOS(stream));
		}

		stream.Seek(map.Header.OffsetDialogue, SeekOrigin.Begin);
		if (map.Header.DialogueLength > 0)
		{
			map.Dialogue = MapDialogueReader.Read(stream, map.Header.DialogueLength);
			map.MapName = ExtractMapName(map.Dialogue) ?? name;

			for (int i = 0; i < map.Dialogue.DialogueSections.Count; i++)
			{
				map.Dialogue.Entries.Add(MapDialogueSectionReader.Read(i, map.Dialogue.DialogueSections[i]));
			}
		}
		else
		{
			map.MapName = name;
		}

		stream.Seek(map.Header.OffsetIcons, SeekOrigin.Begin);
		for (int i = 0; i < map.Header.NumIcons; i++)
		{
			map.Icons.Add(MapRecordReaders.ReadMapIcon(stream));
		}

		stream.Seek(map.Header.OffsetShop, SeekOrigin.Begin);
		if (map.Header.ShopLength > 0)
		{
			map.Shop = MapShopReader.Read(stream);
		}

		return map;
	}

	/// <summary>
	/// The map's display name lives inside the dialogue section itself,
	/// not the header — this scans every dialogue section for one that
	/// starts with the two-byte marker 0x17 0x80 (a known opcode from the
	/// existing opcode set — see G2DataGUI.Domain.Maps.Opcodes; the
	/// section framing here is deliberately NOT decoding through that
	/// opcode reader yet, so this marker check is done directly on the
	/// raw bytes rather than via IMapOpcode), extracts the text between
	/// it and the next 0x17, and skips any candidate containing "to "
	/// (transition prompts like "go to X" apparently share the same
	/// marker and need filtering out) — ported exactly as originally
	/// found, heuristic and all.
	/// </summary>
	private static string? ExtractMapName(MapDialogue dialogue)
	{
		foreach (byte[] section in dialogue.DialogueSections)
		{
			if (section.Length <= 0 || section[0] != 0x17 || section[1] != 0x80)
			{
				continue;
			}

			List<byte> bytes = [];

			for (int i = 4; i < section.Length && section[i] != 0x17; i++)
			{
				bytes.Add(section[i]);
			}

			string text = Encoding.UTF8.GetString(bytes.ToArray());

			if (!text.Contains("to "))
			{
				return text;
			}
		}

		return null;
	}
}
