using G2DataGUI.Domain.Items;
using G2DataGUI.IO.Extensions;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Common;

namespace G2DataGUI.Serialization.Items;

/// <summary>
/// Implements both the single-record and collection shapes: reading "the
/// next item header, with its optional Equipment/Usable resolved via
/// pointer" is meaningful on its own (IDataReader), and ITEM.BIN as a
/// whole is just that operation run Count times (ICollectionDataReader).
/// This is the case the IDataReader/ICollectionDataReader split was made
/// for — Items doesn't need two unrelated classes to express both.
/// </summary>
public sealed class ItemReader : IDataReader<Item>, ICollectionDataReader<Item>
{
	/// <summary>Exact count of records in ITEM.BIN — the file has no
	/// terminator, so unlike Moves/Skills this can't be read-until-EOF
	/// (trailing Equipment/Usable bytes would be misread as more header
	/// records). Matches the original Items.NumberOfItems constant.</summary>
	public const int Count = 0x31F;

	private const int InvalidOffset = -1; // on-disk 0xFFFFFFFF read as a signed int

	public Item Read(Stream stream)
	{
		Item item = new()
		{
			Name = FixedString.ReadLeftPadded(stream, Item.MaxNameLength),
			Description = FixedString.ReadRightPadded(stream, Item.MaxDescriptionLength),
			Stats = ItemStatsSerializer.Read(stream)
		};

		int equipmentOffset = stream.ReadRawInt();
		int usableOffset = stream.ReadRawInt();

		_ = stream.ReadRawUInt(); // on-disk id — discarded, see Item.Id

		long headerEndPosition = stream.Position;

		if (equipmentOffset > InvalidOffset && equipmentOffset != 0)
		{
			stream.Seek(equipmentOffset, SeekOrigin.Begin);
			item.Equipment = EquipmentSerializer.Read(stream);
		}

		if (usableOffset > InvalidOffset && usableOffset != 0)
		{
			stream.Seek(usableOffset, SeekOrigin.Begin);
			item.Usable = UsableSerializer.Read(stream);
		}

		stream.Seek(headerEndPosition, SeekOrigin.Begin);

		return item;
	}

	public async Task<Item> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);

	// Explicit implementation: IDataReader<Item>.Read and
	// ICollectionDataReader<Item>.Read can't both be plain "Read(Stream)"
	// overloads (same signature, different return type isn't a legal C#
	// overload) — only one can be the implicit implementation. The single-
	// record Read above is the one callers reach for by default; the
	// collection form is still reachable as ((ICollectionDataReader<Item>)reader).Read(stream)
	// or, normally, through Repository<T,TId> which asks for the interface directly.
	IReadOnlyList<Item> ICollectionDataReader<Item>.Read(Stream stream)
	{
		List<Item> result = [];

		for (int index = 0; index < Count; index++)
		{
			Item item = Read(stream);
			item.Id = index;
			result.Add(item);
		}

		return result;
	}

	async Task<IReadOnlyList<Item>> ICollectionDataReader<Item>.ReadAsync(Stream stream, CancellationToken cancellationToken) =>
		await Task.Run(() => ((ICollectionDataReader<Item>)this).Read(stream), cancellationToken).ConfigureAwait(false);
}
