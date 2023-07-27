using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;

public class MapEntry
{
    public uint Id { get; set; }
    public Vector3 Position { get; set; }
	public float Direction { get; set; }
	public float Unknown1 { get; set; }
	public int Unknown2 { get; set; }
	public int Unknown3 { get; set; }

	public static MapEntry ReadMapEntry(Stream reader)
	{
		MapEntry entry = new()
		{
			Id = reader.ReadRawUInt(),
			Position = Vector3.ReadVector3(reader),
			Direction = reader.ReadRawFloat(),
			Unknown1 = reader.ReadRawFloat(),
			Unknown2 = reader.ReadRawInt(),
			Unknown3 = reader.ReadRawInt(),
		};

		return entry;
	}

	public void WriteMapEntry(Stream writer)
	{
		writer.WriteRawUInt(Id);
		Position.WriteVector3(writer);
		writer.WriteRawFloat(Direction);
		writer.WriteRawFloat(Unknown1);
		writer.WriteRawInt(Unknown2);
		writer.WriteRawInt(Unknown3);
	}
}
