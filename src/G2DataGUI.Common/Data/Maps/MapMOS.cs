using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;

public class MapMOS
{
    public ushort Id { get; set; }
    public ushort Index { get; set; }
	public int Unknown1 { get; set; }
	public Vector3 Position { get; set; }
	public int Unknown2 { get; set; }
	public int Unknown3 { get; set; }
	public int Unknown4 { get; set; }
	public int Unknown5 { get; set; }
	public short Unknown6 { get; set; }

	public static MapMOS ReadMapMOS(Stream reader)
	{
		MapMOS mos = new()
		{
			Id = reader.ReadRawUShort(),
			Index = reader.ReadRawUShort(),
			Unknown1 = reader.ReadRawInt(),
			Position = Vector3.ReadVector3(reader),
			Unknown2 = reader.ReadRawInt(),
			Unknown3 = reader.ReadRawInt(),
			Unknown4 = reader.ReadRawInt(),
			Unknown5 = reader.ReadRawInt(),
			Unknown6 = reader.ReadRawShort(),
		};

		return mos;
	}

	public void WriteMapMOS(Stream writer)
	{
		writer.WriteRawUShort(Id);
		writer.WriteRawUShort(Index);
		writer.WriteRawInt(Unknown1);
		Position.WriteVector3(writer);
		writer.WriteRawInt(Unknown2);
		writer.WriteRawInt(Unknown3);
		writer.WriteRawInt(Unknown4);
		writer.WriteRawInt(Unknown5);
		writer.WriteRawShort(Unknown6);
	}
}
