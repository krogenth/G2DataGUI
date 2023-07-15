using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;

public class MapInstance
{
    public ushort Id { get; set; }
    public ushort Index { get; set; }
	public short Unknown1 { get; set; }
	public byte Translation { get; set; }
	public byte Unknown2 { get; set; }
	public Vector3 Position { get; set; }
	public Vector3 Angle { get; set; }
	public Vector3 C { get; set; }
	public float Rotation { get; set; }

	public static MapInstance ReadMapInstance(Stream reader)
	{
		MapInstance instance = new()
		{
			Id = reader.ReadRawUShort(),
			Index = reader.ReadRawUShort(),
			Unknown1 = reader.ReadRawShort(),
			Translation = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Position = Vector3.ReadVector3(reader),
			Angle = Vector3.ReadVector3(reader),
			C = Vector3.ReadVector3(reader),
			Rotation = reader.ReadRawFloat(),
		};

		return instance;
	}

	public void WriteMapInstance(Stream writer)
	{
		writer.WriteRawUShort(Id);
		writer.WriteRawUShort(Index);
		writer.WriteRawShort(Unknown1);
		writer.WriteRawByte(Translation);
		writer.WriteRawByte(Unknown2);
		Position.WriteVector3(writer);
		Angle.WriteVector3(writer);
		C.WriteVector3(writer);
		writer.WriteRawFloat(Rotation);
	}
}
