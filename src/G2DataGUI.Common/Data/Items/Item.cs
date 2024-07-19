using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Items;

internal class ItemData
{
    private static readonly uint _itemObjectByteSize = 0x50;
    public static readonly uint ItemPointerOffset = Items.NumberOfItems * _itemObjectByteSize;
    public static readonly uint InvalidOffset = 0xFFFFFFFF;
}

public class Item : BaseContainer
{
    private FixedLengthName _name;
    private FixedLengthDescription _description;
    public ItemStats Stats { get; private set; }
    private ItemOffsets _offsets;
    public EquipmentStats Equipment { get; set; } = null;
    public UsableStats Usable { get; set; } = null;
    public uint ID { get; set; }

    public string Name
    {
        get => _name.Name;
        set
        {
            _name.Name = value;
            NotifyPropertyChanged(nameof(Name));
        }
    }
    public uint MaxNameLength { get => FixedLengthName.MaxLength; }
    public string Description { get => _description.Description; set => _description.Description = value; }
    public uint MaxDescriptionLength { get => FixedLengthDescription.MaxLength; }
    public bool HasEquipment
    {
        get => Equipment != null;
        set
        {
            if (value && Equipment == null)
			{
				Equipment = new EquipmentStats();
			}
			else if (!value && Equipment != null)
			{
				Equipment = null;
			}

			NotifyPropertyChanged(nameof(HasEquipment));
        }
    }
    public bool HasUsable
    {
        get => Usable != null;
        set
        {
            if (value && Usable == null)
			{
				Usable = new UsableStats();
			}
			else if (!value && Usable != null)
			{
				Usable = null;
			}

			NotifyPropertyChanged(nameof(HasUsable));
        }
    }

    public static Item ReadItem(Stream reader)
    {
        Item item = new()
        {
            _name = FixedLengthName.ReadFixedLengthName(reader),
            _description = FixedLengthDescription.ReadFixedLengthDescription(reader),
            Stats = ItemStats.ReadItemStats(reader),
            _offsets = ItemOffsets.ReadItemOffsets(reader),
            ID = reader.ReadRawUInt(),
        };

        var currentPos = reader.Position;
        if (item._offsets.EquipmentOffset > 0)
        {
            reader.Seek(item._offsets.EquipmentOffset, SeekOrigin.Begin);
            item.Equipment = EquipmentStats.ReadEquipmentStats(reader);
        }

        if (item._offsets.UsableOffset > 0)
        {
            reader.Seek(item._offsets.UsableOffset, SeekOrigin.Begin);
            item.Usable = UsableStats.ReadUsableStats(reader);
        }

        reader.Seek(currentPos, SeekOrigin.Begin);

        return item;
    }

    public void WriteItem(Stream writer, ref uint ptrOffset)
    {
        _name.WriteFixedLengthName(writer);
        _description.WriteFixedLengthDescription(writer);
        Stats.WriteItemStats(writer);

        if (Equipment != null)
        {
            writer.WriteRawUInt(ptrOffset + ItemData.ItemPointerOffset);
            ptrOffset += EquipmentStats.ByteSize;
        }
        else
        {
            writer.WriteRawUInt(ItemData.InvalidOffset);
        }

        if (Usable != null)
        {
            writer.WriteRawUInt(ptrOffset + ItemData.ItemPointerOffset);
            ptrOffset += UsableStats.ByteSize;
        }
        else
        {
            writer.WriteRawUInt(ItemData.InvalidOffset);
        }

        writer.WriteRawUInt(ID);
    }

    public void WriteEquipmentAndUsable(Stream writer)
    {
        if (Equipment != null)
        {
            Equipment.WriteEquipmentStats(writer);
        }

        if (Usable != null)
        {
            Usable.WriteUsableStats(writer);
        }
    }
}