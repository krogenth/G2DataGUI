using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;

public class MapShop
{
    public ushort MapId { get; set; }
    public ushort Categories { get; set; }
	public MapShopSection Weapons { get; set; }
	public MapShopSection Armors { get; set; }
	public MapShopSection Jewelry { get; set; }
	public MapShopSection Items { get; set; }
	public MapShopSection Regionals { get; set; }

	public static MapShop ReadMapShop(Stream reader)
	{
		MapShop shop = new()
		{
			MapId = reader.ReadRawUShort(),
			Categories = reader.ReadRawUShort(),
			Weapons = MapShopSection.ReadMapShopSection(reader),
			Armors = MapShopSection.ReadMapShopSection(reader),
			Jewelry = MapShopSection.ReadMapShopSection(reader),
			Items = MapShopSection.ReadMapShopSection(reader),
			Regionals = MapShopSection.ReadMapShopSection(reader),
		};
		return shop;
	}

	public void WriteMapShop(Stream writer)
	{
		writer.WriteRawUShort(MapId);
		writer.WriteRawUShort(Categories);
		Weapons.WriteMapShopSection(writer);
		Armors.WriteMapShopSection(writer);
		Jewelry.WriteMapShopSection(writer);
		Items.WriteMapShopSection(writer);
		Regionals.WriteMapShopSection(writer);
	}
}
