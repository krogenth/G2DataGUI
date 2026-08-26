using G2DataGUI.Domain.Maps;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Maps;

public static class MapHeaderReader
{
	public static MapHeader Read(Stream stream) => new()
	{
		HeaderLength = stream.ReadRawUInt(),
		Unknown1 = stream.ReadRawUInt(),
		NumMapEntries = stream.ReadRawUInt(),
		OffsetMapEntries = stream.ReadRawUInt(),
		Unknown2 = stream.ReadRawUInt(),
		Unknown3 = stream.ReadRawUInt(),
		NumInstances = stream.ReadRawUInt(),
		OffsetInstances = stream.ReadRawUInt(),
		NumHTA = stream.ReadRawUInt(),
		OffsetHTA = stream.ReadRawUInt(),
		NumScripts = stream.ReadRawUInt(),
		OffsetScripts = stream.ReadRawUInt(),
		Unknown4 = stream.ReadRawUInt(),
		Unknown5 = stream.ReadRawUInt(),
		NumEnemyPos = stream.ReadRawUInt(),
		OffsetEnemyPos = stream.ReadRawUInt(),
		Unknown6 = stream.ReadRawUInt(),
		Unknown7 = stream.ReadRawUInt(),
		NumEnemyGroups = stream.ReadRawUInt(),
		OffsetEnemyGroups = stream.ReadRawUInt(),
		NumMOS = stream.ReadRawUInt(),
		OffsetMOS = stream.ReadRawUInt(),
		Unknown8 = stream.ReadRawUInt(),
		Unknown9 = stream.ReadRawUInt(),
		Unknown10 = stream.ReadRawUInt(),
		Unknown11 = stream.ReadRawUInt(),
		Unknown12 = stream.ReadRawUInt(),
		Unknown13 = stream.ReadRawUInt(),
		Unknown14 = stream.ReadRawUInt(),
		Unknown15 = stream.ReadRawUInt(),
		DialogueLength = stream.ReadRawUInt(),
		OffsetDialogue = stream.ReadRawUInt(),
		Unknown16 = stream.ReadRawUInt(),
		Unknown17 = stream.ReadRawUInt(),
		Unknown18 = stream.ReadRawUInt(),
		Unknown19 = stream.ReadRawUInt(),
		Unknown20 = stream.ReadRawUInt(),
		Unknown21 = stream.ReadRawUInt(),
		NumIcons = stream.ReadRawUInt(),
		OffsetIcons = stream.ReadRawUInt(),
		Unknown22 = stream.ReadRawUInt(),
		Unknown23 = stream.ReadRawUInt(),
		ShopLength = stream.ReadRawUInt(),
		OffsetShop = stream.ReadRawUInt(),
		Unknown24 = stream.ReadRawUInt(),
		Unknown25 = stream.ReadRawUInt(),
		Unknown26 = stream.ReadRawUInt(),
		Unknown27 = stream.ReadRawUInt(),
		Unknown28 = stream.ReadRawUInt(),
		Unknown29 = stream.ReadRawUInt(),
	};

	public static void Write(Stream stream, MapHeader header)
	{
		stream.WriteRawUInt(header.HeaderLength);
		stream.WriteRawUInt(header.Unknown1);
		stream.WriteRawUInt(header.NumMapEntries);
		stream.WriteRawUInt(header.OffsetMapEntries);
		stream.WriteRawUInt(header.Unknown2);
		stream.WriteRawUInt(header.Unknown3);
		stream.WriteRawUInt(header.NumInstances);
		stream.WriteRawUInt(header.OffsetInstances);
		stream.WriteRawUInt(header.NumHTA);
		stream.WriteRawUInt(header.OffsetHTA);
		stream.WriteRawUInt(header.NumScripts);
		stream.WriteRawUInt(header.OffsetScripts);
		stream.WriteRawUInt(header.Unknown4);
		stream.WriteRawUInt(header.Unknown5);
		stream.WriteRawUInt(header.NumEnemyPos);
		stream.WriteRawUInt(header.OffsetEnemyPos);
		stream.WriteRawUInt(header.Unknown6);
		stream.WriteRawUInt(header.Unknown7);
		stream.WriteRawUInt(header.NumEnemyGroups);
		stream.WriteRawUInt(header.OffsetEnemyGroups);
		stream.WriteRawUInt(header.NumMOS);
		stream.WriteRawUInt(header.OffsetMOS);
		stream.WriteRawUInt(header.Unknown8);
		stream.WriteRawUInt(header.Unknown9);
		stream.WriteRawUInt(header.Unknown10);
		stream.WriteRawUInt(header.Unknown11);
		stream.WriteRawUInt(header.Unknown12);
		stream.WriteRawUInt(header.Unknown13);
		stream.WriteRawUInt(header.Unknown14);
		stream.WriteRawUInt(header.Unknown15);
		stream.WriteRawUInt(header.DialogueLength);
		stream.WriteRawUInt(header.OffsetDialogue);
		stream.WriteRawUInt(header.Unknown16);
		stream.WriteRawUInt(header.Unknown17);
		stream.WriteRawUInt(header.Unknown18);
		stream.WriteRawUInt(header.Unknown19);
		stream.WriteRawUInt(header.Unknown20);
		stream.WriteRawUInt(header.Unknown21);
		stream.WriteRawUInt(header.NumIcons);
		stream.WriteRawUInt(header.OffsetIcons);
		stream.WriteRawUInt(header.Unknown22);
		stream.WriteRawUInt(header.Unknown23);
		stream.WriteRawUInt(header.ShopLength);
		stream.WriteRawUInt(header.OffsetShop);
		stream.WriteRawUInt(header.Unknown24);
		stream.WriteRawUInt(header.Unknown25);
		stream.WriteRawUInt(header.Unknown26);
		stream.WriteRawUInt(header.Unknown27);
		stream.WriteRawUInt(header.Unknown28);
		stream.WriteRawUInt(header.Unknown29);
	}
}
