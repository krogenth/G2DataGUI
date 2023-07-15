using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;

public class MapIcon
{
    public byte Id { get; set; }
    public byte Unknown1 { get; set; }
	public short Unknown2 { get; set; }
	public Vector3 Position { get; set; }
	public float Unknown3 { get; set; }
	public float YAngle { get; set; }
	public short Item1 { get; set; }
	public short Item2 { get; set; }
	public short Item3 { get; set; }
	public short Flag { get; set; }

	public static MapIcon ReadMapIcon(Stream reader)
	{
		MapIcon icon = new()
		{
			Id = reader.ReadRawByte(),
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawShort(),
			Position = Vector3.ReadVector3(reader),
			Unknown3 = reader.ReadRawFloat(),
			YAngle = reader.ReadRawFloat(),
			Item1 = reader.ReadRawShort(),
			Item2 = reader.ReadRawShort(),
			Item3 = reader.ReadRawShort(),
			Flag = reader.ReadRawShort(),
		};

		return icon;
	}

	public void WriteMapIcon(Stream writer)
	{
		writer.WriteRawByte(Id);
		writer.WriteRawByte(Unknown1);
		writer.WriteRawShort(Unknown2);
		Position.WriteVector3(writer);
		writer.WriteRawFloat(Unknown3);
		writer.WriteRawFloat(YAngle);
		writer.WriteRawShort(Item1);
		writer.WriteRawShort(Item2);
		writer.WriteRawShort(Item3);
		writer.WriteRawShort(Flag);
	}
}
