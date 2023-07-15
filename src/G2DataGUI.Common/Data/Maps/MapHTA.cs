using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;

public class MapHTA
{
    public byte Shape { get; set; }
    public byte Type { get; set; }
	public byte Trigger { get; set; }
	public byte Unknown1 { get; set; }
	public int Unknown2 { get; set; }
	public byte Attribute { get; set; }
	public byte Unknown3 { get; set; }
	public short Unknown4 { get; set; }
	public int Unknown5 { get; set; }
	public Vector3 Minimum { get; set; }
	public Vector3 Maximum { get; set; }
	public Vector3 Unknown6 { get; set; }
	public int Unknown7 { get; set; }
	public int Unknown8 { get; set; }
	public int Unknown9 { get; set; }
	public int Unknown10 { get; set; }
	public int Unknown11 { get; set; }

	public static MapHTA ReadMapHTA(Stream reader)
	{
		MapHTA hta = new()
		{
			Shape = reader.ReadRawByte(),
			Type = reader.ReadRawByte(),
			Trigger = reader.ReadRawByte(),
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawInt(),
			Attribute = reader.ReadRawByte(),
			Unknown3 = reader.ReadRawByte(),
			Unknown4 = reader.ReadRawShort(),
			Unknown5 = reader.ReadRawInt(),
			Minimum = Vector3.ReadVector3(reader),
			Maximum = Vector3.ReadVector3(reader),
			Unknown6 = Vector3.ReadVector3(reader),
			Unknown7 = reader.ReadRawInt(),
			Unknown8 = reader.ReadRawInt(),
			Unknown9 = reader.ReadRawInt(),
			Unknown10 = reader.ReadRawInt(),
			Unknown11 = reader.ReadRawInt(),
		};

		return hta;
	}

	public void WriteMapHTA(Stream writer)
	{
		writer.WriteRawByte(Shape);
		writer.WriteRawByte(Type);
		writer.WriteRawByte(Trigger);
		writer.WriteRawByte(Unknown1);
		writer.WriteRawInt(Unknown2);
		writer.WriteRawByte(Attribute);
		writer.WriteRawByte(Unknown3);
		writer.WriteRawInt(Unknown5);
		Minimum.WriteVector3(writer);
		Maximum.WriteVector3(writer);
		Unknown6.WriteVector3(writer);
		writer.WriteRawInt(Unknown7);
		writer.WriteRawInt(Unknown8);
		writer.WriteRawInt(Unknown9);
		writer.WriteRawInt(Unknown10);
		writer.WriteRawInt(Unknown11);
	}
}
