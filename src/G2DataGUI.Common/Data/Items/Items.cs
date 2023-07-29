using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Data.Errors;
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
		_ = ReadItemsAsync();
    }

	public void Save() => WriteItems();

	public void Reload() => _ = ReadItemsAsync();

	private async Task ReadItemsAsync() =>
		await Task.Run(ReadItems).ConfigureAwait(false);

	private void ReadItems()
    {
		try
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
				item.ID = index;
				GameItems.Add(item);
			}

			CollectionRefreshed?.Invoke(this, EventArgs.Empty);
		}
        catch (Exception ex)
		{
			Errors.Errors.Instance.AddError(new Error("Items", ex.Message));
		}
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
