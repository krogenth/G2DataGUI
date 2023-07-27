using System.IO;

using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;

public class MapEnemyPosition
{
    public ushort Index { get; set; }
    public short Unknown1 { get; set; }
	public Vector2 Minimum { get; set; }
	public Vector2 Maximum { get; set; }
	public Vector3 Position { get; set; }
	public Vector3 Unknown2 { get; set; }
	public Vector3 Unknown3 { get; set; }
	public Vector3 Unknown4 { get; set; }
	public Vector3 Unknown5 { get; set; }
	public Vector3 Unknown6 { get; set; }
	public Vector3 Unknown7 { get; set; }

	public static MapEnemyPosition ReadMapEnemyPosition(Stream reader)
	{
		MapEnemyPosition enemyPosition = new()
		{
			Index = reader.ReadRawUShort(),
			Unknown1 = reader.ReadRawShort(),
			Minimum = Vector2.ReadVector2(reader),
			Maximum = Vector2.ReadVector2(reader),
			Position = Vector3.ReadVector3(reader),
			Unknown2 = Vector3.ReadVector3(reader),
			Unknown3 = Vector3.ReadVector3(reader),
			Unknown4 = Vector3.ReadVector3(reader),
			Unknown5 = Vector3.ReadVector3(reader),
			Unknown6 = Vector3.ReadVector3(reader),
			Unknown7 = Vector3.ReadVector3(reader),
		};

		return enemyPosition;
	}

	public void WriteMapEnemyPosition(Stream writer)
	{
		writer.WriteRawUShort(Index);
		writer.WriteRawShort(Unknown1);
		Minimum.WriteVector2(writer);
		Maximum.WriteVector2(writer);
		Position.WriteVector3(writer);
		Unknown2.WriteVector3(writer);
		Unknown3.WriteVector3(writer);
		Unknown4.WriteVector3(writer);
		Unknown5.WriteVector3(writer);
		Unknown6.WriteVector3(writer);
		Unknown7.WriteVector3(writer);
	}
}
