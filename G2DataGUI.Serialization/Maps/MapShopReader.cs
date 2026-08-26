using G2DataGUI.Domain.Maps;
using G2DataGUI.IO.Extensions;
using G2DataGUI.Serialization.Common;

namespace G2DataGUI.Serialization.Maps;

public static class MapShopReader
{
	/// <summary>Confirmed: FixedLengthTitle is always 8 characters.</summary>
	private const int TitleLength = 8;

	public static MapShop Read(Stream stream) => new()
	{
		MapId = stream.ReadRawUShort(),
		Categories = stream.ReadRawUShort(),
		Weapons = ReadSection(stream),
		Armors = ReadSection(stream),
		Jewelry = ReadSection(stream),
		Items = ReadSection(stream),
		Regionals = ReadSection(stream),
	};

	public static void Write(Stream stream, MapShop shop)
	{
		stream.WriteRawUShort(shop.MapId);
		stream.WriteRawUShort(shop.Categories);
		WriteSection(stream, shop.Weapons);
		WriteSection(stream, shop.Armors);
		WriteSection(stream, shop.Jewelry);
		WriteSection(stream, shop.Items);
		WriteSection(stream, shop.Regionals);
	}

	private static MapShopSection ReadSection(Stream stream) => new()
	{
		Title = FixedString.ReadRightPadded(stream, TitleLength),
		Items = ReadItemList(stream),
	};

	private static void WriteSection(Stream stream, MapShopSection section)
	{
		FixedString.WriteRightPadded(stream, section.Title, TitleLength);
		WriteItemList(stream, section.Items);
	}

	/// <summary>The +0x800 on every non-zero offset (see MapShopItemList's
	/// own doc comment) is applied/removed here, once, in a loop — the
	/// original had this as 12 nearly-identical hand-written if-blocks per
	/// direction; same behavior, considerably less to misread.</summary>
	private static MapShopItemList ReadItemList(Stream stream)
	{
		MapShopItemList list = new();

		for (int i = 0; i < list.ItemOffsets.Length; i++)
		{
			ushort raw = stream.ReadRawUShort();

			list.ItemOffsets[i] = raw > 0 ? (ushort)(raw - MapShopItemList.ItemOffsetOffset) : raw;
		}

		return list;
	}

	private static void WriteItemList(Stream stream, MapShopItemList list)
	{
		foreach (ushort offset in list.ItemOffsets)
		{
			stream.WriteRawUShort(offset > 0 ? (ushort)(offset + MapShopItemList.ItemOffsetOffset) : offset);
		}
	}
}
