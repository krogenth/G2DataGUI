using System.IO;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Maps;
public class MapShopItemList
{
	public ushort Item1Offset { get; set; }
	public ushort Item2Offset { get; set; }
	public ushort Item3Offset { get; set; }
	public ushort Item4Offset { get; set; }
	public ushort Item5Offset { get; set; }
	public ushort Item6Offset { get; set; }
	public ushort Item7Offset { get; set; }
	public ushort Item8Offset { get; set; }
	public ushort Item9Offset { get; set; }
	public ushort Item10Offset { get; set; }
	public ushort Item11Offset { get; set; }
	public ushort Item12Offset { get; set; }

	// for whatever reason, shop items are offset by 0x0800
	// so item 0x0100 would actually be 0x0900 on disk
	public static ushort ItemOffsetOffset { get => 0x800; }

	public static MapShopItemList ReadMapShopItemList(Stream reader)
	{
		MapShopItemList list = new()
		{
			Item1Offset = reader.ReadRawUShort(),
			Item2Offset = reader.ReadRawUShort(),
			Item3Offset = reader.ReadRawUShort(),
			Item4Offset = reader.ReadRawUShort(),
			Item5Offset = reader.ReadRawUShort(),
			Item6Offset = reader.ReadRawUShort(),
			Item7Offset = reader.ReadRawUShort(),
			Item8Offset = reader.ReadRawUShort(),
			Item9Offset = reader.ReadRawUShort(),
			Item10Offset = reader.ReadRawUShort(),
			Item11Offset = reader.ReadRawUShort(),
			Item12Offset = reader.ReadRawUShort(),
		};

		if (list.Item1Offset > 0)
		{
			list.Item1Offset -= ItemOffsetOffset;
		}

		if (list.Item2Offset > 0)
		{
			list.Item2Offset -= ItemOffsetOffset;
		}

		if (list.Item3Offset > 0)
		{
			list.Item3Offset -= ItemOffsetOffset;
		}

		if (list.Item4Offset > 0)
		{
			list.Item4Offset -= ItemOffsetOffset;
		}

		if (list.Item5Offset > 0)
		{
			list.Item5Offset -= ItemOffsetOffset;
		}

		if (list.Item6Offset > 0)
		{
			list.Item6Offset -= ItemOffsetOffset;
		}

		if (list.Item7Offset > 0)
		{
			list.Item7Offset -= ItemOffsetOffset;
		}

		if (list.Item8Offset > 0)
		{
			list.Item8Offset -= ItemOffsetOffset;
		}

		if (list.Item9Offset > 0)
		{
			list.Item9Offset -= ItemOffsetOffset;
		}

		if (list.Item10Offset > 0)
		{
			list.Item10Offset -= ItemOffsetOffset;
		}

		if (list.Item11Offset > 0)
		{
			list.Item11Offset -= ItemOffsetOffset;
		}

		if (list.Item12Offset > 0)
		{
			list.Item12Offset -= ItemOffsetOffset;
		}

		return list;
	}

	public void WritemapShopItemList(Stream writer)
	{
		writer.WriteRawUShort(Item1Offset > 0 ? (ushort)(Item1Offset + ItemOffsetOffset) : Item1Offset);
		writer.WriteRawUShort(Item2Offset > 0 ? (ushort)(Item2Offset + ItemOffsetOffset) : Item2Offset);
		writer.WriteRawUShort(Item3Offset > 0 ? (ushort)(Item3Offset + ItemOffsetOffset) : Item3Offset);
		writer.WriteRawUShort(Item4Offset > 0 ? (ushort)(Item4Offset + ItemOffsetOffset) : Item4Offset);
		writer.WriteRawUShort(Item5Offset > 0 ? (ushort)(Item5Offset + ItemOffsetOffset) : Item5Offset);
		writer.WriteRawUShort(Item6Offset > 0 ? (ushort)(Item6Offset + ItemOffsetOffset) : Item6Offset);
		writer.WriteRawUShort(Item7Offset > 0 ? (ushort)(Item7Offset + ItemOffsetOffset) : Item7Offset);
		writer.WriteRawUShort(Item8Offset > 0 ? (ushort)(Item8Offset + ItemOffsetOffset) : Item8Offset);
		writer.WriteRawUShort(Item9Offset > 0 ? (ushort)(Item9Offset + ItemOffsetOffset) : Item9Offset);
		writer.WriteRawUShort(Item10Offset > 0 ? (ushort)(Item10Offset + ItemOffsetOffset) : Item10Offset);
		writer.WriteRawUShort(Item11Offset > 0 ? (ushort)(Item11Offset + ItemOffsetOffset) : Item11Offset);
		writer.WriteRawUShort(Item12Offset > 0 ? (ushort)(Item12Offset + ItemOffsetOffset) : Item12Offset);
	}
}
