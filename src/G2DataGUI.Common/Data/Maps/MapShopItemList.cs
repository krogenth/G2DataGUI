using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;
public class MapShopItemList
{
	public ushort Item1 { get; set; }
	public ushort Item2 { get; set; }
	public ushort Item3 { get; set; }
	public ushort Item4 { get; set; }
	public ushort Item5 { get; set; }
	public ushort Item6 { get; set; }
	public ushort Item7 { get; set; }
	public ushort Item8 { get; set; }
	public ushort Item9 { get; set; }
	public ushort Item10 { get; set; }
	public ushort Item11 { get; set; }
	public ushort Item12 { get; set; }

	public static MapShopItemList ReadMapShopItemList(Stream reader)
	{
		MapShopItemList list = new()
		{
			Item1 = reader.ReadRawUShort(),
			Item2 = reader.ReadRawUShort(),
			Item3 = reader.ReadRawUShort(),
			Item4 = reader.ReadRawUShort(),
			Item5 = reader.ReadRawUShort(),
			Item6 = reader.ReadRawUShort(),
			Item7 = reader.ReadRawUShort(),
			Item8 = reader.ReadRawUShort(),
			Item9 = reader.ReadRawUShort(),
			Item10 = reader.ReadRawUShort(),
			Item11 = reader.ReadRawUShort(),
			Item12 = reader.ReadRawUShort(),
		};
		return list;
	}

	public void WritemapShopItemList(Stream writer)
	{
		writer.WriteRawUShort(Item1);
		writer.WriteRawUShort(Item2);
		writer.WriteRawUShort(Item3);
		writer.WriteRawUShort(Item4);
		writer.WriteRawUShort(Item5);
		writer.WriteRawUShort(Item6);
		writer.WriteRawUShort(Item7);
		writer.WriteRawUShort(Item8);
		writer.WriteRawUShort(Item9);
		writer.WriteRawUShort(Item10);
		writer.WriteRawUShort(Item11);
		writer.WriteRawUShort(Item12);
	}
}
