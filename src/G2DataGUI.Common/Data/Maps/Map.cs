using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Maps;

public class Map
{
    public MapHeader Header;
    public ObservableCollection<MapEntry> Entries { get; set; } = new();
    public ObservableCollection<MapInstance> Instances { get; set; } = new();
    public ObservableCollection<MapHTA> HTAs { get; set; } = new();
    public ObservableCollection<MapEnemyPosition> EnemyPositions { get; set; } = new();
    public ObservableCollection<MapEnemyGroup> EnemyGroups { get; set; } = new();
    public ObservableCollection<MapMOS> MOSs { get; set; } = new();
    public ObservableCollection<MapIcon> Icons { get; set; } = new();
    public MapShop Shop { get; set; } = null;

    public string MapName { get; set; } = "";
    public string FileLocation { get; set; } = "";

    public static Map ReadMap(FileStream reader, string file)
    {
		Map map = new()
		{
			Header = MapHeader.ReadMapHeader(reader),
			FileLocation = file
		};

		reader.Seek(map.Header.OffsetMapEntries, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumMapEntries; index++)
		{
			map.Entries.Add(MapEntry.ReadMapEntry(reader));
		}

		reader.Seek(map.Header.OffsetInstances, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumInstances; index++)
		{
			map.Instances.Add(MapInstance.ReadMapInstance(reader));
		}

		reader.Seek(map.Header.OffsetInstances, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumHTA; index++)
		{
			map.HTAs.Add(MapHTA.ReadMapHTA(reader));
		}

		reader.Seek(map.Header.OffsetEnemyPos, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumEnemyPos; index++)
		{
			map.EnemyPositions.Add(MapEnemyPosition.ReadMapEnemyPosition(reader));
		}

		reader.Seek(map.Header.OffsetEnemyGroups, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumEnemyGroups; index++)
		{
			map.EnemyGroups.Add(MapEnemyGroup.ReadMapEnemyGroup(reader));
		}

		reader.Seek(map.Header.OffsetMOS, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumMOS; index++)
		{
			map.MOSs.Add(MapMOS.ReadMapMOS(reader));
		}

		reader.Seek(map.Header.OffsetIcons, SeekOrigin.Begin);
		for (var index = 0; index < map.Header.NumIcons; index++)
		{
			map.Icons.Add(MapIcon.ReadMapIcon(reader));
		}

		reader.Seek(map.Header.OffsetShop, SeekOrigin.Begin);
		if (map.Header.ShopLength > 0)
		{
			map.Shop = MapShop.ReadMapShop(reader);
		}

        return map;
    }
}
