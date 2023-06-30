using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Items;

public class Items
{
    public static Items Instance { get; } = new();
    private ObservableCollection<Item> _items = new();
    public event EventHandler CollectionRefreshed;
    public const uint NumberOfItems = 0x31F;

    private Items()
    {
        ReadItems();
    }

    public void Save()
    {
        WriteItems();
    }

    public void Reload()
    {
        ReadItems();
    }

    private void ReadItems()
    {
        _items.Clear();
        using (FileStream reader = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.Items, FileMode.Open))
        using (MemoryStream memReader = new MemoryStream())
        {
            reader.CopyTo(memReader);
            memReader.Seek(0, SeekOrigin.Begin);
            for (uint index = 0; index < NumberOfItems; index++)
            {
                Item item = Item.ReadItem(memReader);
                if (item != null)
                {
                    item.ID = index;
                    _items.Add(item);
                }
            }
        }
    }

    private void WriteItems()
    {
        using (FileStream writer = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.Items, FileMode.OpenOrCreate))
        {
            uint pointer = 0;
            foreach (var item in _items)
            {
                item.WriteItem(writer, ref pointer);
            }

            foreach (var item in _items)
            {
                item.WriteEquipmentAndUsable(writer);
            }
        }
    }

    public ObservableCollection<Item> GetItems() { return _items; }
}