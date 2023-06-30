using System.IO;
using System.Runtime.InteropServices;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Items;

internal class ItemData
{
    private static readonly uint ItemObjectByteSize = 0x50;
    public static readonly uint ItemPointerOffset = Items.NumberOfItems * ItemObjectByteSize;
    public static readonly uint BadOffset = 0xFFFFFFFF;
}

public class Item : BaseContainer
{
    private FixedLengthName _name;
    private FixedLengthDescription _description;
    private ItemStats _stats;
    private ItemOffsets _offsets;
    private Equipment _equipment = null;
    private Usable _usable = null;
    private uint _id;

    public static Item ReadItem(Stream reader)
    {
        Item item = new Item();

        FixedLengthName? name = reader.ReadStruct<FixedLengthName>();
        if (name != null) item._name = name.Value;

        FixedLengthDescription? description = reader.ReadStruct<FixedLengthDescription>();
        if (description != null) item._description = description.Value;

        ItemStats? stats = reader.ReadStruct<ItemStats>();
        if (stats != null) item._stats = stats.Value;

        ItemOffsets? offsets = reader.ReadStruct<ItemOffsets>();
        if (offsets != null) item._offsets = offsets.Value;

        item._id = reader.ReadUInt();

        var currentPos = reader.Position;
        if (item._offsets.EquipmentOffset > 0)
        {
            reader.Seek(item._offsets.EquipmentOffset, SeekOrigin.Begin);
            item._equipment = Equipment.ReadEquipment(reader);
        }
        if (item._offsets.UsableOffset > 0)
        {
            reader.Seek(item._offsets.UsableOffset, SeekOrigin.Begin);
            item._usable = Usable.ReadUsable(reader);
        }
        reader.Seek(currentPos, SeekOrigin.Begin);

        return item;
    }

    public void WriteItem(Stream writer, ref uint ptrOffset)
    {
        writer.WriteStruct(_name);
        writer.WriteStruct(_description);
        writer.WriteStruct(_stats);

        if (_equipment != null)
        {
            writer.WriteUInt(ptrOffset + ItemData.ItemPointerOffset);
            ptrOffset += (uint)Marshal.SizeOf(typeof(EquipmentStats));
        }
        else
        {
            writer.WriteUInt(ItemData.BadOffset);
        }

        if (_usable != null)
        {
            writer.WriteUInt(ptrOffset + ItemData.ItemPointerOffset);
            ptrOffset += (uint)Marshal.SizeOf(typeof(UsableStats));
        }
        else
        {
            writer.WriteUInt(ItemData.BadOffset);
        }

        writer.WriteUInt(_id);
    }

    public void WriteEquipmentAndUsable(Stream writer)
    {
        if (_equipment != null)
        {
            _equipment.WriteEquipment(writer);
        }
        if (_usable != null)
        {
            _usable.WriteUsable(writer);
        }
    }

    public string Name { get => _name.Name; set { _name.Name = value; NotifyPropertyChanged(nameof(Name)); } }
    public int MaxNameLength { get => _name.MaxLength; }
    public string Description { get => _description.Description; set => _description.Description = value; }
    public int MaxDescriptionLength { get => _description.MaxLength; }
    public byte EntryType { get => _stats.EntryType; set => _stats.EntryType = value; }
    public byte StatsUnknown1 { get => _stats.Unknown1; set => _stats.Unknown1 = value; }
    public byte StatsUnknown2 { get => _stats.Unknown2; set => _stats.Unknown2 = value; }
    public byte StatsUnknown3 { get => _stats.Unknown3; set => _stats.Unknown3 = value; }
    public byte Icon { get => _stats.Icon; set => _stats.Icon = value; }
    public byte StatsUnknown4 { get => _stats.Unknown4; set => _stats.Unknown4 = value; }
    public int Price { get => _stats.Price; set => _stats.Price = value; }
    public bool HasEquipment
    {
        get => _equipment != null;
        set
        {
            if (value && _equipment == null) _equipment = new Equipment();
            else if (!value && _equipment != null) _equipment = null;
            NotifyPropertyChanged(nameof(HasEquipment));
        }
    }
    public Equipment EquipmentObject { get => _equipment; }
    public bool HasUsable
    {
        get => _usable != null;
        set
        {
            if (value && _usable == null) _usable = new Usable();
            else if (!value && _usable != null) _usable = null;
            NotifyPropertyChanged(nameof(HasUsable));
        }
    }
    public Usable UsableObject { get => _usable; }
    public uint ID { get => _id; set => _id = value; }
}