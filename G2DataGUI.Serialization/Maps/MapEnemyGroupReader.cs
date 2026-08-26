using G2DataGUI.Domain.Maps;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Maps;

public static class MapEnemyGroupReader
{
	public static MapEnemyGroup Read(Stream stream) => new()
	{
		Index = stream.ReadRawUShort(),
		Unknown1 = ReadFirstUnknown(stream),
		Enemies = ReadEnemyList(stream),
		Unknown2 = ReadSecondUnknown(stream),
	};

	public static void Write(Stream stream, MapEnemyGroup group)
	{
		stream.WriteRawUShort(group.Index);
		WriteFirstUnknown(stream, group.Unknown1);
		WriteEnemyList(stream, group.Enemies);
		WriteSecondUnknown(stream, group.Unknown2);
	}

	private static MapEnemyGroupFirstUnknown ReadFirstUnknown(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawShort(),
		Unknown2 = stream.ReadRawShort(),
		Unknown3 = stream.ReadRawShort(),
		Unknown4 = stream.ReadRawShort(),
		Unknown5 = stream.ReadRawShort(),
		Unknown6 = stream.ReadRawFloat(),
		Unknown7 = stream.ReadRawShort(),
		Unknown8 = stream.ReadRawShort(),
		Unknown9 = stream.ReadRawShort(),
		Unknown10 = stream.ReadRawShort(),
	};

	private static void WriteFirstUnknown(Stream stream, MapEnemyGroupFirstUnknown value)
	{
		stream.WriteRawShort(value.Unknown1);
		stream.WriteRawShort(value.Unknown2);
		stream.WriteRawShort(value.Unknown3);
		stream.WriteRawShort(value.Unknown4);
		stream.WriteRawShort(value.Unknown5);
		stream.WriteRawFloat(value.Unknown6);
		stream.WriteRawShort(value.Unknown7);
		stream.WriteRawShort(value.Unknown8);
		stream.WriteRawShort(value.Unknown9);
		stream.WriteRawShort(value.Unknown10);
	}

	private static MapEnemyGroupSecondUnknown ReadSecondUnknown(Stream stream) => new()
	{
		Unknown1 = stream.ReadRawShort(),
		Unknown2 = stream.ReadRawShort(),
		Unknown3 = stream.ReadRawShort(),
		Unknown4 = stream.ReadRawShort(),
		Unknown5 = stream.ReadRawShort(),
		Unknown6 = stream.ReadRawShort(),
		Unknown7 = stream.ReadRawShort(),
		Unknown8 = stream.ReadRawShort(),
		Unknown9 = stream.ReadRawShort(),
		Unknown10 = stream.ReadRawShort(),
		Unknown11 = stream.ReadRawShort(),
		Unknown12 = stream.ReadRawShort(),
		Unknown13 = stream.ReadRawShort(),
		Unknown14 = stream.ReadRawShort(),
		Unknown15 = stream.ReadRawShort(),
		Unknown16 = stream.ReadRawShort(),
		Unknown17 = stream.ReadRawShort(),
		Unknown18 = stream.ReadRawShort(),
		Unknown19 = stream.ReadRawShort(),
		Unknown20 = stream.ReadRawShort(),
	};

	private static void WriteSecondUnknown(Stream stream, MapEnemyGroupSecondUnknown value)
	{
		stream.WriteRawShort(value.Unknown1);
		stream.WriteRawShort(value.Unknown2);
		stream.WriteRawShort(value.Unknown3);
		stream.WriteRawShort(value.Unknown4);
		stream.WriteRawShort(value.Unknown5);
		stream.WriteRawShort(value.Unknown6);
		stream.WriteRawShort(value.Unknown7);
		stream.WriteRawShort(value.Unknown8);
		stream.WriteRawShort(value.Unknown9);
		stream.WriteRawShort(value.Unknown10);
		stream.WriteRawShort(value.Unknown11);
		stream.WriteRawShort(value.Unknown12);
		stream.WriteRawShort(value.Unknown13);
		stream.WriteRawShort(value.Unknown14);
		stream.WriteRawShort(value.Unknown15);
		stream.WriteRawShort(value.Unknown16);
		stream.WriteRawShort(value.Unknown17);
		stream.WriteRawShort(value.Unknown18);
		stream.WriteRawShort(value.Unknown19);
		stream.WriteRawShort(value.Unknown20);
	}

	private static MapEnemyList ReadEnemyList(Stream stream) => new()
	{
		Enemy1 = ReadEnemyType(stream),
		Enemy2 = ReadEnemyType(stream),
		Enemy3 = ReadEnemyType(stream),
		Enemy4 = ReadEnemyType(stream),
	};

	private static void WriteEnemyList(Stream stream, MapEnemyList list)
	{
		WriteEnemyType(stream, list.Enemy1);
		WriteEnemyType(stream, list.Enemy2);
		WriteEnemyType(stream, list.Enemy3);
		WriteEnemyType(stream, list.Enemy4);
	}

	private static MapEnemyType ReadEnemyType(Stream stream) => new()
	{
		Index = stream.ReadRawShort(),
		EnemyCount = stream.ReadRawShort(),
		EnemyOffset = stream.ReadRawShort(),
		Unknown1 = stream.ReadRawShort(),
		Unknown2 = stream.ReadRawShort(),
		Unknown3 = stream.ReadRawShort(),
		Unknown4 = stream.ReadRawShort(),
	};

	private static void WriteEnemyType(Stream stream, MapEnemyType type)
	{
		stream.WriteRawShort(type.Index);
		stream.WriteRawShort(type.EnemyCount);
		stream.WriteRawShort(type.EnemyOffset);
		stream.WriteRawShort(type.Unknown1);
		stream.WriteRawShort(type.Unknown2);
		stream.WriteRawShort(type.Unknown3);
		stream.WriteRawShort(type.Unknown4);
	}
}
