namespace G2DataGUI.Domain.Maps;

/// <summary>Map.Shop — null when the map has no shop (Header.ShopLength
/// == 0). Grouped with MapShopSection/MapShopItemList in this one file —
/// same reasoning as MapEnemyGroup's cluster, they only ever appear as
/// parts of a MapShop.</summary>
public sealed class MapShop
{
	public ushort MapId { get; set; }
	public ushort Categories { get; set; }
	public MapShopSection Weapons { get; set; } = new();
	public MapShopSection Armors { get; set; } = new();
	public MapShopSection Jewelry { get; set; } = new();
	public MapShopSection Items { get; set; } = new();
	public MapShopSection Regionals { get; set; } = new();
}

/// <summary>Title is always 8 characters on disk (FixedLengthTitle,
/// confirmed).</summary>
public sealed class MapShopSection
{
	public string Title { get; set; } = string.Empty;
	public MapShopItemList Items { get; set; } = new();
}

/// <summary>Up to 12 items for sale. Offsets are stored on disk with a
/// mysterious +0x800 added to every non-zero entry (ItemOffsetOffset) —
/// ported exactly as originally found, not just noted: the reader
/// subtracts it going in, the (not yet implemented — see
/// MapShopSectionSerializer) writer would need to add it back going out.</summary>
public sealed class MapShopItemList
{
	public const ushort ItemOffsetOffset = 0x800;

	public ushort[] ItemOffsets { get; set; } = new ushort[12];
}
