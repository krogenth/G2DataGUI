using System.IO;
using System.Runtime.InteropServices;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Items;

internal class ItemData
{
    private static readonly uint ItemObjectByteSize = 0x50;
    public static readonly uint ItemPointerOffset = Items.NumberOfItems * ItemObjectByteSize;
    public static readonly uint InvalidOffset = 0xFFFFFFFF;
}

public class Item : BaseContainer
{
    private FixedLengthName _name;
    private FixedLengthDescription _description;
    public ItemStats Stats { get; private set; }
    private ItemOffsets _offsets;
    public Equipment Equipment { get; set; } = null;
    public Usable Usable { get; set; } = null;
    public uint ID { get; set; }

    public string Name { get => _name.Name; set { _name.Name = value; NotifyPropertyChanged(nameof(Name)); } }
    public int MaxNameLength { get => _name.MaxLength; }
    public string Description { get => _description.Description; set => _description.Description = value; }
    public int MaxDescriptionLength { get => _description.MaxLength; }
    public bool HasEquipment
    {
        get => Equipment != null;
        set
        {
            if (value && Equipment == null) Equipment = new Equipment();
            else if (!value && Equipment != null) Equipment = null;
            NotifyPropertyChanged(nameof(HasEquipment));
        }
    }
    public bool HasUsable
    {
        get => Usable != null;
        set
        {
            if (value && Usable == null) Usable = new Usable();
            else if (!value && Usable != null) Usable = null;
            NotifyPropertyChanged(nameof(HasUsable));
        }
    }

    public static Item ReadItem(Stream reader)
    {
        Item item = new Item();

        FixedLengthName? name = reader.ReadStruct<FixedLengthName>();
        if (name != null) item._name = name.Value;

        FixedLengthDescription? description = reader.ReadStruct<FixedLengthDescription>();
        if (description != null) item._description = description.Value;

        item.Stats = ItemStats.ReadItemStats(reader);

        ItemOffsets? offsets = reader.ReadStruct<ItemOffsets>();
        if (offsets != null) item._offsets = offsets.Value;

        item.ID = reader.ReadRawUInt();

        var currentPos = reader.Position;
        if (item._offsets.EquipmentOffset > 0)
        {
            reader.Seek(item._offsets.EquipmentOffset, SeekOrigin.Begin);
            item.Equipment = Equipment.ReadEquipmentStats(reader);
        }
        if (item._offsets.UsableOffset > 0)
        {
            reader.Seek(item._offsets.UsableOffset, SeekOrigin.Begin);
            item.Usable = Usable.ReadUsableStats(reader);
        }
        reader.Seek(currentPos, SeekOrigin.Begin);

        return item;
    }

    public void WriteItem(Stream writer, ref uint ptrOffset)
    {
        writer.WriteStruct(_name);
        writer.WriteStruct(_description);
        Stats.WriteItemStats(writer);

        if (Equipment != null)
        {
            writer.WriteRawUInt(ptrOffset + ItemData.ItemPointerOffset);
            ptrOffset += Equipment.ByteSize;
        }
        else
        {
            writer.WriteRawUInt(ItemData.InvalidOffset);
        }

        if (Usable != null)
        {
            writer.WriteRawUInt(ptrOffset + ItemData.ItemPointerOffset);
            ptrOffset += Usable.ByteSize;
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