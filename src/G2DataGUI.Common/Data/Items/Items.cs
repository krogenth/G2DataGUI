using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Paths;

namespace G2DataGUI.Common.Data.Items;

public class Items
{
    public static Items Instance { get; } = new();
    public ObservableCollection<Item> GameItems { get; private set; } = new();
    public event EventHandler CollectionRefreshed;
    public const uint NumberOfItems = 0x31F;

    private Items()
    {
        ReadItems();
    }

	public void Save() => WriteItems();

	public void Reload() => ReadItems();

	private async Task ReadItems()
    {
        GameItems.Clear();
        using FileStream reader = File.Open(
            Version.Instance.RootDataDirectory + GamePaths.ItemsPath,
            FileMode.Open,
            FileAccess.Read);
        using MemoryStream memReader = new();
        reader.CopyTo(memReader);
        memReader.Seek(0, SeekOrigin.Begin);
        for (uint index = 0; index < NumberOfItems; index++)
        {
            Item item = Item.ReadItem(memReader);
            if (item != null)
            {
                item.ID = index;
                GameItems.Add(item);
            }
        }

        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    private void WriteItems()
    {
        using FileStream writer = File.Open(
            Version.Instance.RootDataDirectory + GamePaths.ItemsPath,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite);
        uint pointer = 0;
        foreach (var item in GameItems)
        {
            item.WriteItem(writer, ref pointer);
        }

        foreach (var item in GameItems)
        {
            item.WriteEquipmentAndUsable(writer);
        }
    }
}
