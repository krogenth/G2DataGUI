using System.IO;

using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;

public class MapHeader
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

	public static MapHeader ReadMapHeader(Stream reader)
	{
		MapHeader header = new()
		{
			HeaderLength = reader.ReadRawUInt(),
			Unknown1 = reader.ReadRawUInt(),
			NumMapEntries = reader.ReadRawUInt(),
			OffsetMapEntries = reader.ReadRawUInt(),
			Unknown2 = reader.ReadRawUInt(),
			Unknown3 = reader.ReadRawUInt(),
			NumInstances = reader.ReadRawUInt(),
			OffsetInstances = reader.ReadRawUInt(),
			NumHTA = reader.ReadRawUInt(),
			OffsetHTA = reader.ReadRawUInt(),
			NumScripts = reader.ReadRawUInt(),
			OffsetScripts = reader.ReadRawUInt(),
			Unknown4 = reader.ReadRawUInt(),
			Unknown5 = reader.ReadRawUInt(),
			NumEnemyPos = reader.ReadRawUInt(),
			OffsetEnemyPos = reader.ReadRawUInt(),
			Unknown6 = reader.ReadRawUInt(),
			Unknown7 = reader.ReadRawUInt(),
			NumEnemyGroups = reader.ReadRawUInt(),
			OffsetEnemyGroups = reader.ReadRawUInt(),
			NumMOS = reader.ReadRawUInt(),
			OffsetMOS = reader.ReadRawUInt(),
			Unknown8 = reader.ReadRawUInt(),
			Unknown9 = reader.ReadRawUInt(),
			Unknown10 = reader.ReadRawUInt(),
			Unknown11 = reader.ReadRawUInt(),
			Unknown12 = reader.ReadRawUInt(),
			Unknown13 = reader.ReadRawUInt(),
			Unknown14 = reader.ReadRawUInt(),
			Unknown15 = reader.ReadRawUInt(),
			DialogueLength = reader.ReadRawUInt(),
			OffsetDialogue = reader.ReadRawUInt(),
			Unknown16 = reader.ReadRawUInt(),
			Unknown17 = reader.ReadRawUInt(),
			Unknown18 = reader.ReadRawUInt(),
			Unknown19 = reader.ReadRawUInt(),
			Unknown20 = reader.ReadRawUInt(),
			Unknown21 = reader.ReadRawUInt(),
			NumIcons = reader.ReadRawUInt(),
			OffsetIcons = reader.ReadRawUInt(),
			Unknown22 = reader.ReadRawUInt(),
			Unknown23 = reader.ReadRawUInt(),
			ShopLength = reader.ReadRawUInt(),
			OffsetShop = reader.ReadRawUInt(),
			Unknown24 = reader.ReadRawUInt(),
			Unknown25 = reader.ReadRawUInt(),
			Unknown26 = reader.ReadRawUInt(),
			Unknown27 = reader.ReadRawUInt(),
			Unknown28 = reader.ReadRawUInt(),
			Unknown29 = reader.ReadRawUInt(),
		};
		return header;
	}

	public void WriteMapHeader(Stream writer)
	{
		writer.WriteRawUInt(HeaderLength);
		writer.WriteRawUInt(Unknown1);
		writer.WriteRawUInt(NumMapEntries);
		writer.WriteRawUInt(OffsetMapEntries);
		writer.WriteRawUInt(Unknown2);
		writer.WriteRawUInt(Unknown3);
		writer.WriteRawUInt(NumInstances);
		writer.WriteRawUInt(OffsetInstances);
		writer.WriteRawUInt(NumHTA);
		writer.WriteRawUInt(OffsetHTA);
		writer.WriteRawUInt(NumScripts);
		writer.WriteRawUInt(OffsetScripts);
		writer.WriteRawUInt(Unknown4);
		writer.WriteRawUInt(Unknown5);
		writer.WriteRawUInt(NumEnemyPos);
		writer.WriteRawUInt(OffsetEnemyPos);
		writer.WriteRawUInt(Unknown6);
		writer.WriteRawUInt(Unknown7);
		writer.WriteRawUInt(NumEnemyGroups);
		writer.WriteRawUInt(OffsetEnemyGroups);
		writer.WriteRawUInt(NumMOS);
		writer.WriteRawUInt(OffsetMOS);
		writer.WriteRawUInt(Unknown8);
		writer.WriteRawUInt(Unknown9);
		writer.WriteRawUInt(Unknown10);
		writer.WriteRawUInt(Unknown11);
		writer.WriteRawUInt(Unknown12);
		writer.WriteRawUInt(Unknown13);
		writer.WriteRawUInt(Unknown14);
		writer.WriteRawUInt(Unknown15);
		writer.WriteRawUInt(DialogueLength);
		writer.WriteRawUInt(OffsetDialogue);
		writer.WriteRawUInt(Unknown16);
		writer.WriteRawUInt(Unknown17);
		writer.WriteRawUInt(Unknown18);
		writer.WriteRawUInt(Unknown19);
		writer.WriteRawUInt(Unknown20);
		writer.WriteRawUInt(Unknown21);
		writer.WriteRawUInt(NumIcons);
		writer.WriteRawUInt(OffsetIcons);
		writer.WriteRawUInt(Unknown22);
		writer.WriteRawUInt(Unknown23);
		writer.WriteRawUInt(ShopLength);
		writer.WriteRawUInt(OffsetShop);
		writer.WriteRawUInt(Unknown24);
		writer.WriteRawUInt(Unknown25);
		writer.WriteRawUInt(Unknown26);
		writer.WriteRawUInt(Unknown27);
		writer.WriteRawUInt(Unknown28);
		writer.WriteRawUInt(Unknown29);
	}
}
