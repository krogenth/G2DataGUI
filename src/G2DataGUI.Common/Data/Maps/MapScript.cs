using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;
public class MapScript
{
	// used for UI list
	public int Index { get; set; }

	public byte[] Unknown1 { get; set; }
	public Vector3 Offset { get; set; } = new();
	public float Direction { get; set; }
	public byte TransitionEffect { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public short MapID { get; set; }
	public short Unknown5 { get; set; }
	public byte Unknown6 { get; set; }
	public byte Unknown7 { get; set; }
	public short Unknown8 { get; set; }
	public byte Unknown9 { get; set; }
	public byte Unknown10 { get; set; }
	public byte Unknown11 { get; set; }
	public byte Unknown12 { get; set; }
	public byte Unknown13 { get; set; }
	public byte Unknown14 { get; set; }
	public byte Unknown15 { get; set; }
	public byte Unknown16 { get; set; }
	public byte Unknown17 { get; set; }
	public byte[] Unknown18 { get; set; }

	public static uint Unknown1Length => 40;
	public static uint Unknown18Length => 15;

	public static MapScript ReadMapScript(Stream reader, int index)
	{
		MapScript mapScript = new()
		{
			Index = index,
			Unknown1 = reader.ReadRawByteArray(Unknown1Length),
			Offset = Vector3.ReadVector3(reader),
			Direction = reader.ReadRawFloat(),
			TransitionEffect = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Unknown3 = reader.ReadRawByte(),
			Unknown4 = reader.ReadRawByte(),
			MapID = reader.ReadRawShort(),
			Unknown5 = reader.ReadRawShort(),
			Unknown6 = reader.ReadRawByte(),
			Unknown7 = reader.ReadRawByte(),
			Unknown8 = reader.ReadRawShort(),
			Unknown9 = reader.ReadRawByte(),
			Unknown10 = reader.ReadRawByte(),
			Unknown11 = reader.ReadRawByte(),
			Unknown12 = reader.ReadRawByte(),
			Unknown13 = reader.ReadRawByte(),
			Unknown14 = reader.ReadRawByte(),
			Unknown15 = reader.ReadRawByte(),
			Unknown16 = reader.ReadRawByte(),
			Unknown17 = reader.ReadRawByte(),
			Unknown18 = reader.ReadRawByteArray(Unknown18Length),
		};
		return mapScript;
	}

	public void WriteMapScript(Stream writer)
	{
		writer.WriteRawByteArray(Unknown1);
		Offset.WriteVector3(writer);
		writer.WriteRawFloat(Direction);
		writer.WriteRawByte(TransitionEffect);
		writer.WriteRawByte(Unknown2);
		writer.WriteRawByte(Unknown3);
		writer.WriteRawByte(Unknown4);
		writer.WriteRawShort(MapID);
		writer.WriteRawShort(Unknown5);
		writer.WriteRawByte(Unknown6);
		writer.WriteRawByte(Unknown7);
		writer.WriteRawShort(Unknown8);
		writer.WriteRawByte(Unknown9);
		writer.WriteRawByte(Unknown10);
		writer.WriteRawByte(Unknown11);
		writer.WriteRawByte(Unknown12);
		writer.WriteRawByte(Unknown13);
		writer.WriteRawByte(Unknown14);
		writer.WriteRawByte(Unknown15);
		writer.WriteRawByte(Unknown16);
		writer.WriteRawByte(Unknown17);
		writer.WriteRawByteArray(Unknown18);
	}
}
