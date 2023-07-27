using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;

public class MapEnemyGroup
{
    public ushort Index { get; set; }
    public MapEnemyGroupFirstUnknown Unknown1 { get; set; }
	public MapEnemyList Enemies { get; set; }
	public MapEnemyGroupSecondUnknown Unknown2 { get; set; }

	public static MapEnemyGroup ReadMapEnemyGroup(Stream reader)
	{
		MapEnemyGroup enemyGroup = new()
		{
			Index = reader.ReadRawUShort(),
			Unknown1 = MapEnemyGroupFirstUnknown.ReadMapEnemyGroupFirstUnknown(reader),
			Enemies = MapEnemyList.ReadMapEnemyList(reader),
			Unknown2 = MapEnemyGroupSecondUnknown.ReadMapEnemyGroupSecondUnknown(reader),
		};

		return enemyGroup;
	}

	public void WriteMapEnemyGroup(Stream writer)
	{
		writer.WriteRawUShort(Index);
		Unknown1.WriteMapEnemyGroupFirstUnknown(writer);
		Enemies.WriteMapEnemyList(writer);
		Unknown2.WriteMapEnemyGroupSecondUnknown(writer);
	}
}
