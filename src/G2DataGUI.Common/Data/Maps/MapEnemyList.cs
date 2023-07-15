using System.IO;

namespace G2DataGUI.Common.Data.Maps;

public class MapEnemyList
{
    public MapEnemyType Enemy1 { get; set; }
    public MapEnemyType Enemy2 { get; set; }
	public MapEnemyType Enemy3 { get; set; }
	public MapEnemyType Enemy4 { get; set; }

	public static MapEnemyList ReadMapEnemyList(Stream reader)
	{
		MapEnemyList enemyList = new()
		{
			Enemy1 = MapEnemyType.ReadMapEnemyType(reader),
			Enemy2 = MapEnemyType.ReadMapEnemyType(reader),
			Enemy3 = MapEnemyType.ReadMapEnemyType(reader),
			Enemy4 = MapEnemyType.ReadMapEnemyType(reader),
		};

		return enemyList;
	}

	public void WriteMapEnemyList(Stream writer)
	{
		Enemy1.WriteMapEnemyType(writer);
		Enemy2.WriteMapEnemyType(writer);
		Enemy3.WriteMapEnemyType(writer);
		Enemy4.WriteMapEnemyType(writer);
	}
}
