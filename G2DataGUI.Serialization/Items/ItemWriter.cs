using G2DataGUI.Domain.Items;
using G2DataGUI.IO.Extensions;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Common;

namespace G2DataGUI.Serialization.Items;

/// <summary>
/// Collection-only — deliberately does not also implement IDataWriter&lt;Item&gt;.
/// Writing a single Item's header needs to know the absolute file offset
/// its Equipment/Usable block will land at, which depends on how many
/// Equipment/Usable blocks precede it in the file; that's inherently a
/// whole-collection computation (two passes: headers, then variable
/// blocks), not something one item can do in isolation. Not every format
/// needs both interfaces — see PORTING.md.
/// </summary>
public sealed class ItemWriter : ICollectionDataWriter<Item>
{
	private static readonly uint _headerTableSize = (uint)ItemReader.Count * ItemRecordSize;
	private const uint ItemRecordSize = 0x50;

	public void Write(Stream stream, IEnumerable<Item> items)
	{
		List<Item> list = [.. items];
		uint pointer = 0;

		foreach (Item item in list)
		{
			FixedString.WriteLeftPadded(stream, item.Name, Item.MaxNameLength);
			FixedString.WriteRightPadded(stream, item.Description, Item.MaxDescriptionLength);
			ItemStatsSerializer.Write(stream, item.Stats);

			if (item.Equipment is not null)
			{
				stream.WriteRawInt((int)(pointer + _headerTableSize));
				pointer += Equipment.ByteSize;
			}
			else
			{
				stream.WriteRawInt(-1);
			}

			if (item.Usable is not null)
			{
				stream.WriteRawInt((int)(pointer + _headerTableSize));
				pointer += Usable.ByteSize;
			}
			else
			{
				stream.WriteRawInt(-1);
			}

			stream.WriteRawUInt((uint)item.Id);
		}

		foreach (Item item in list)
		{
			if (item.Equipment is not null)
			{
				EquipmentSerializer.Write(stream, item.Equipment);
			}

			if (item.Usable is not null)
			{
				UsableSerializer.Write(stream, item.Usable);
			}
		}
	}

	public async Task WriteAsync(Stream stream, IEnumerable<Item> items, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Write(stream, items), cancellationToken).ConfigureAwait(false);
}
