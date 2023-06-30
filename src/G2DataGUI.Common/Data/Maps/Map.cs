using G2DataGUI.IO.Streams;
using System.Collections.Generic;
using System.IO;

namespace G2DataGUI.Common.Data.Maps
{
    public class Map
    {
        public Header header;
        public List<MapEntry> Entries { get; set; } = new List<MapEntry>();
        public List<Instance> Instances { get; set; } = new List<Instance>();
        public List<HTA> HTAs { get; set; } = new List<HTA>();
        public List<EnemyPosition> EnemyPositions { get; set; } = new List<EnemyPosition>();
        public List<EnemyGroup> EnemyGroups { get; set; } = new List<EnemyGroup>();
        public List<MOS> MOSs { get; set; } = new List<MOS>();
        public List<Icon> Icons { get; set; } = new List<Icon>();
        public Shop? Shop { get; set; } = null;

        public string MapName { get; set; } = "";
        public string FileLocation { get; set; } = "";

        public static Map ReadMap(FileStream reader, string file)
        {
            Map map = new Map();
            map.FileLocation = file;

            Header? header = reader.ReadStruct<Header>();
            if (header != null) map.header = header.Value;
            else return null;

            if (map.header.NumMapEntries > 0)
            {
                reader.Seek(map.header.OffsetMapEntries, SeekOrigin.Begin);
                for (var index = 0; index < map.header.NumMapEntries; index++)
                {
                    MapEntry? entry = reader.ReadStruct<MapEntry>();
                    if (entry != null) map.Entries.Add(entry.Value);
                }
            }

            if (map.header.NumInstances > 0)
            {
                reader.Seek(map.header.OffsetInstances, SeekOrigin.Begin);
                for (var index = 0; index < map.header.NumInstances; index++)
                {
                    Instance? instance = reader.ReadStruct<Instance>();
                    if (instance != null) map.Instances.Add(instance.Value);
                }
            }

            if (map.header.NumHTA > 0)
            {
                reader.Seek(map.header.OffsetHTA, SeekOrigin.Begin);
                for (var index = 0; index < map.header.NumHTA; index++)
                {
                    HTA? hta = reader.ReadStruct<HTA>();
                    if (hta != null) map.HTAs.Add(hta.Value);
                }
            }

            if (map.header.NumEnemyPos > 0)
            {
                reader.Seek(map.header.OffsetEnemyPos, SeekOrigin.Begin);
                for (var index = 0; index < map.header.NumEnemyPos; index++)
                {
                    EnemyPosition? position = reader.ReadStruct<EnemyPosition>();
                    if (position != null) map.EnemyPositions.Add(position.Value);
                }
            }

            if (map.header.NumEnemyGroups > 0)
            {
                reader.Seek(map.header.OffsetEnemyGroups, SeekOrigin.Begin);
                for (var index = 0; index < map.header.NumEnemyGroups; index++)
                {
                    EnemyGroup? group = reader.ReadStruct<EnemyGroup>();
                    if (group != null) map.EnemyGroups.Add(group.Value);
                }
            }

            if (map.header.NumMOS > 0)
            {
                reader.Seek(map.header.OffsetMOS, SeekOrigin.Begin);
                for (var index = 0; index < map.header.NumMOS; index++)
                {
                    MOS? mos = reader.ReadStruct<MOS>();
                    if (mos != null) map.MOSs.Add(mos.Value);
                }
            }
            
            if (map.header.NumIcons > 0)
            {
                reader.Seek(map.header.OffsetIcons, SeekOrigin.Begin);
                for (var index = 0; index < map.header.NumIcons; index++)
                {
                    Icon? icon = reader.ReadStruct<Icon>();
                    if (icon != null) map.Icons.Add(icon.Value);
                }
            }
            

            if (map.header.LengthShop > 0)
            {
                reader.Seek(map.header.OffsetShop, SeekOrigin.Begin);
                Shop? shop = reader.ReadStruct<Shop>();
                if (shop != null) map.Shop = shop.Value;
            }

            return map;
        }
    }
}