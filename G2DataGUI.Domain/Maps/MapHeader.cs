namespace G2DataGUI.Domain.Maps;

/// <summary>
/// .mdt file header — a count+offset pair per section, pointing at where
/// each of Map's other lists live in the file. Most of the padding fields
/// between sections are still unidentified (Unknown1-29) — ported as-is
/// rather than guessing at names for fields nothing currently reads.
/// </summary>
public sealed class MapHeader
{
	public uint HeaderLength { get; set; }
	public uint Unknown1 { get; set; }

	public uint NumMapEntries { get; set; }
	public uint OffsetMapEntries { get; set; }

	public uint Unknown2 { get; set; }
	public uint Unknown3 { get; set; }

	public uint NumInstances { get; set; }
	public uint OffsetInstances { get; set; }

	public uint NumHTA { get; set; }
	public uint OffsetHTA { get; set; }

	public uint NumScripts { get; set; }
	public uint OffsetScripts { get; set; }

	public uint Unknown4 { get; set; }
	public uint Unknown5 { get; set; }

	public uint NumEnemyPos { get; set; }
	public uint OffsetEnemyPos { get; set; }

	public uint Unknown6 { get; set; }
	public uint Unknown7 { get; set; }

	public uint NumEnemyGroups { get; set; }
	public uint OffsetEnemyGroups { get; set; }

	public uint NumMOS { get; set; }
	public uint OffsetMOS { get; set; }

	public uint Unknown8 { get; set; }
	public uint Unknown9 { get; set; }

	public uint Unknown10 { get; set; }
	public uint Unknown11 { get; set; }

	public uint Unknown12 { get; set; }
	public uint Unknown13 { get; set; }

	public uint Unknown14 { get; set; }
	public uint Unknown15 { get; set; }

	public uint DialogueLength { get; set; }
	public uint OffsetDialogue { get; set; }

	public uint Unknown16 { get; set; }
	public uint Unknown17 { get; set; }

	public uint Unknown18 { get; set; }
	public uint Unknown19 { get; set; }

	public uint Unknown20 { get; set; }
	public uint Unknown21 { get; set; }

	public uint NumIcons { get; set; }
	public uint OffsetIcons { get; set; }

	public uint Unknown22 { get; set; }
	public uint Unknown23 { get; set; }

	public uint ShopLength { get; set; }
	public uint OffsetShop { get; set; }

	public uint Unknown24 { get; set; }
	public uint Unknown25 { get; set; }

	public uint Unknown26 { get; set; }
	public uint Unknown27 { get; set; }

	public uint Unknown28 { get; set; }
	public uint Unknown29 { get; set; }
}
