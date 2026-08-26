using G2DataGUI.Domain.Maps;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Maps;

/// <summary>Read/write for MapEntry/MapInstance/MapHTA/MapScript/
/// MapEnemyPosition/MapMOS/MapIcon — grouped into one file since each is a
/// small, self-contained record with no further sub-structure, the same
/// clustering the domain types themselves use.</summary>
public static class MapRecordReaders
{
	public static MapEntry ReadMapEntry(Stream stream) => new()
	{
		Id = stream.ReadRawUInt(),
		Position = stream.ReadRawVector3(),
		Direction = stream.ReadRawFloat(),
		Unknown1 = stream.ReadRawFloat(),
		Unknown2 = stream.ReadRawInt(),
		Unknown3 = stream.ReadRawInt(),
	};

	public static void WriteMapEntry(Stream stream, MapEntry entry)
	{
		stream.WriteRawUInt(entry.Id);
		stream.WriteRawVector3(entry.Position);
		stream.WriteRawFloat(entry.Direction);
		stream.WriteRawFloat(entry.Unknown1);
		stream.WriteRawInt(entry.Unknown2);
		stream.WriteRawInt(entry.Unknown3);
	}

	public static MapInstance ReadMapInstance(Stream stream) => new()
	{
		Id = stream.ReadRawUShort(),
		Index = stream.ReadRawUShort(),
		Unknown1 = stream.ReadRawShort(),
		Translation = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		Position = stream.ReadRawVector3(),
		Angle = stream.ReadRawVector3(),
		C = stream.ReadRawVector3(),
		Rotation = stream.ReadRawFloat(),
	};

	public static void WriteMapInstance(Stream stream, MapInstance instance)
	{
		stream.WriteRawUShort(instance.Id);
		stream.WriteRawUShort(instance.Index);
		stream.WriteRawShort(instance.Unknown1);
		stream.WriteRawByte(instance.Translation);
		stream.WriteRawByte(instance.Unknown2);
		stream.WriteRawVector3(instance.Position);
		stream.WriteRawVector3(instance.Angle);
		stream.WriteRawVector3(instance.C);
		stream.WriteRawFloat(instance.Rotation);
	}

	public static MapHTA ReadMapHTA(Stream stream) => new()
	{
		Shape = stream.ReadRawByte(),
		Type = stream.ReadRawByte(),
		Trigger = stream.ReadRawByte(),
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawInt(),
		Attribute = stream.ReadRawByte(),
		Unknown3 = stream.ReadRawByte(),
		Unknown4 = stream.ReadRawShort(),
		Unknown5 = stream.ReadRawFloat(),
		Minimum = stream.ReadRawVector3(),
		Maximum = stream.ReadRawVector3(),
		Unknown6 = stream.ReadRawVector3(),
		Unknown7 = stream.ReadRawInt(),
		Unknown8 = stream.ReadRawFloat(),
		Unknown9 = stream.ReadRawFloat(),
		Unknown10 = stream.ReadRawFloat(),
		Unknown11 = stream.ReadRawFloat(),
	};

	/// <summary>The version this was ported from skipped writing
	/// Unknown2/Unknown3/Unknown4 here (went straight from Attribute to
	/// Unknown5) — a real bug, not intentional; fixed here rather than
	/// carried forward, since porting a known write-corrupts-the-file bug
	/// silently would be worse than fixing it and saying so.</summary>
	public static void WriteMapHTA(Stream stream, MapHTA hta)
	{
		stream.WriteRawByte(hta.Shape);
		stream.WriteRawByte(hta.Type);
		stream.WriteRawByte(hta.Trigger);
		stream.WriteRawByte(hta.Unknown1);
		stream.WriteRawInt(hta.Unknown2);
		stream.WriteRawByte(hta.Attribute);
		stream.WriteRawByte(hta.Unknown3);
		stream.WriteRawShort(hta.Unknown4);
		stream.WriteRawFloat(hta.Unknown5);
		stream.WriteRawVector3(hta.Minimum);
		stream.WriteRawVector3(hta.Maximum);
		stream.WriteRawVector3(hta.Unknown6);
		stream.WriteRawInt(hta.Unknown7);
		stream.WriteRawFloat(hta.Unknown8);
		stream.WriteRawFloat(hta.Unknown9);
		stream.WriteRawFloat(hta.Unknown10);
		stream.WriteRawFloat(hta.Unknown11);
	}

	public static MapScript ReadMapScript(Stream stream, int index) => new()
	{
		Index = index,
		Unknown1 = stream.ReadRawByteArray(40),
		Offset = stream.ReadRawVector3(),
		Direction = stream.ReadRawFloat(),
		TransitionEffect = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		Unknown3 = stream.ReadRawByte(),
		Unknown4 = stream.ReadRawByte(),
		MapID = stream.ReadRawShort(),
		Unknown5 = stream.ReadRawShort(),
		Unknown6 = stream.ReadRawByte(),
		Unknown7 = stream.ReadRawByte(),
		Unknown8 = stream.ReadRawShort(),
		Unknown9 = stream.ReadRawByte(),
		Unknown10 = stream.ReadRawByte(),
		Unknown11 = stream.ReadRawByte(),
		Unknown12 = stream.ReadRawByte(),
		Unknown13 = stream.ReadRawByte(),
		Unknown14 = stream.ReadRawByte(),
		Unknown15 = stream.ReadRawByte(),
		Unknown16 = stream.ReadRawByte(),
		Unknown17 = stream.ReadRawByte(),
		Unknown18 = stream.ReadRawByteArray(15),
	};

	public static void WriteMapScript(Stream stream, MapScript script)
	{
		stream.WriteRawByteArray(script.Unknown1);
		stream.WriteRawVector3(script.Offset);
		stream.WriteRawFloat(script.Direction);
		stream.WriteRawByte(script.TransitionEffect);
		stream.WriteRawByte(script.Unknown2);
		stream.WriteRawByte(script.Unknown3);
		stream.WriteRawByte(script.Unknown4);
		stream.WriteRawShort(script.MapID);
		stream.WriteRawShort(script.Unknown5);
		stream.WriteRawByte(script.Unknown6);
		stream.WriteRawByte(script.Unknown7);
		stream.WriteRawShort(script.Unknown8);
		stream.WriteRawByte(script.Unknown9);
		stream.WriteRawByte(script.Unknown10);
		stream.WriteRawByte(script.Unknown11);
		stream.WriteRawByte(script.Unknown12);
		stream.WriteRawByte(script.Unknown13);
		stream.WriteRawByte(script.Unknown14);
		stream.WriteRawByte(script.Unknown15);
		stream.WriteRawByte(script.Unknown16);
		stream.WriteRawByte(script.Unknown17);
		stream.WriteRawByteArray(script.Unknown18);
	}

	public static MapEnemyPosition ReadMapEnemyPosition(Stream stream) => new()
	{
		Index = stream.ReadRawUShort(),
		Unknown1 = stream.ReadRawShort(),
		Minimum = stream.ReadRawVector2(),
		Maximum = stream.ReadRawVector2(),
		Position = stream.ReadRawVector3(),
		Unknown2 = stream.ReadRawVector3(),
		Unknown3 = stream.ReadRawVector3(),
		Unknown4 = stream.ReadRawVector3(),
		Unknown5 = stream.ReadRawVector3(),
		Unknown6 = stream.ReadRawVector3(),
		Unknown7 = stream.ReadRawVector3(),
	};

	public static void WriteMapEnemyPosition(Stream stream, MapEnemyPosition position)
	{
		stream.WriteRawUShort(position.Index);
		stream.WriteRawShort(position.Unknown1);
		stream.WriteRawVector2(position.Minimum);
		stream.WriteRawVector2(position.Maximum);
		stream.WriteRawVector3(position.Position);
		stream.WriteRawVector3(position.Unknown2);
		stream.WriteRawVector3(position.Unknown3);
		stream.WriteRawVector3(position.Unknown4);
		stream.WriteRawVector3(position.Unknown5);
		stream.WriteRawVector3(position.Unknown6);
		stream.WriteRawVector3(position.Unknown7);
	}

	public static MapMOS ReadMapMOS(Stream stream) => new()
	{
		Id = stream.ReadRawUShort(),
		Index = stream.ReadRawUShort(),
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		Unknown3 = stream.ReadRawByte(),
		Unknown4 = stream.ReadRawByte(),
		Position = stream.ReadRawVector3(),
		Unknown5 = stream.ReadRawShort(),
		Unknown6 = stream.ReadRawShort(),
		Unknown7 = stream.ReadRawShort(),
		Unknown8 = stream.ReadRawShort(),
		Unknown9 = stream.ReadRawShort(),
		Unknown10 = stream.ReadRawShort(),
	};

	public static void WriteMapMOS(Stream stream, MapMOS mos)
	{
		stream.WriteRawUShort(mos.Id);
		stream.WriteRawUShort(mos.Index);
		stream.WriteRawByte(mos.Unknown1);
		stream.WriteRawByte(mos.Unknown2);
		stream.WriteRawByte(mos.Unknown3);
		stream.WriteRawByte(mos.Unknown4);
		stream.WriteRawVector3(mos.Position);
		stream.WriteRawShort(mos.Unknown5);
		stream.WriteRawShort(mos.Unknown6);
		stream.WriteRawShort(mos.Unknown7);
		stream.WriteRawShort(mos.Unknown8);
		stream.WriteRawShort(mos.Unknown9);
		stream.WriteRawShort(mos.Unknown10);
	}

	public static MapIcon ReadMapIcon(Stream stream) => new()
	{
		Id = stream.ReadRawByte(),
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawShort(),
		Position = stream.ReadRawVector3(),
		Unknown3 = stream.ReadRawFloat(),
		YAngle = stream.ReadRawFloat(),
		Item1Offset = stream.ReadRawShort(),
		Item2Offset = stream.ReadRawShort(),
		Item3Offset = stream.ReadRawShort(),
		Flag = stream.ReadRawShort(),
	};

	public static void WriteMapIcon(Stream stream, MapIcon icon)
	{
		stream.WriteRawByte(icon.Id);
		stream.WriteRawByte(icon.Unknown1);
		stream.WriteRawShort(icon.Unknown2);
		stream.WriteRawVector3(icon.Position);
		stream.WriteRawFloat(icon.Unknown3);
		stream.WriteRawFloat(icon.YAngle);
		stream.WriteRawShort(icon.Item1Offset);
		stream.WriteRawShort(icon.Item2Offset);
		stream.WriteRawShort(icon.Item3Offset);
		stream.WriteRawShort(icon.Flag);
	}
}
