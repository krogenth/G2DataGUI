using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;

public class MapMOS
{
    public ushort Id { get; set; }
    public ushort Index { get; set; }
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public Vector3 Position { get; set; }
	public short Unknown5 { get; set; }
	public short Unknown6 { get; set; }
	public short Unknown7 { get; set; }
	public short Unknown8 { get; set; }
	public short Unknown9 { get; set; }
	public short Unknown10 { get; set; }

	public static MapMOS ReadMapMOS(Stream reader)
	{
		MapMOS mos = new()
		{
			Id = reader.ReadRawUShort(),
			Index = reader.ReadRawUShort(),
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Unknown3 = reader.ReadRawByte(),
			Unknown4 = reader.ReadRawByte(),
			Position = Vector3.ReadVector3(reader),
			Unknown5 = reader.ReadRawShort(),
			Unknown6 = reader.ReadRawShort(),
			Unknown7 = reader.ReadRawShort(),
			Unknown8 = reader.ReadRawShort(),
			Unknown9 = reader.ReadRawShort(),
			Unknown10 = reader.ReadRawShort(),
		};

		return mos;
	}

	public void WriteMapMOS(Stream writer)
	{
		writer.WriteRawUShort(Id);
		writer.WriteRawUShort(Index);
		writer.WriteRawByte(Unknown1);
		writer.WriteRawByte(Unknown2);
		writer.WriteRawByte(Unknown3);
		writer.WriteRawByte(Unknown4);
		Position.WriteVector3(writer);
		writer.WriteRawShort(Unknown5);
		writer.WriteRawShort(Unknown6);
		writer.WriteRawShort(Unknown7);
		writer.WriteRawShort(Unknown8);
		writer.WriteRawShort(Unknown9);
		writer.WriteRawShort(Unknown10);
	}
}
