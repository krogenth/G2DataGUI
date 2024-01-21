using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Data.Errors;

namespace G2DataGUI.Common.Data.Maps;

public class Maps
{
    public static Maps Instance { get; private set; } = new Maps();
	public ObservableCollection<ushort> GameMapIDs { get; private set; } = new();
    public ObservableCollection<Map> GameMaps { get; private set; } = new();
    public event EventHandler CollectionRefreshed;

    private Maps()
    {
		_ = ReadMapsAsync();
    }

	public void Save() => WriteMaps();

	public void Reload() => _ = ReadMapsAsync();

	private async Task ReadMapsAsync() =>
		await Task.Run(ReadMaps).ConfigureAwait(false);

    private void ReadMaps()
    {
		FileStream reader = null;
		try
		{
			GameMaps.Clear();
			GameMapIDs.Clear();
			foreach (var file in Directory.GetFiles(Version.Instance.RootDataDirectory + "map", "*.mdt", SearchOption.AllDirectories))
			{
				reader = File.Open(file, FileMode.Open);
				GameMaps.Add(Map.ReadMap(reader, file));
				var name = new DirectoryInfo(Path.GetDirectoryName(file)).Name;
				GameMapIDs.Add(Convert.ToUInt16(name, 16));
				reader.Dispose();
			}

			CollectionRefreshed?.Invoke(this, EventArgs.Empty);
		}
		catch (Exception ex)
		{
			Errors.Errors.Instance.AddError(new Error("Maps", ex.Message));
		}
		finally
		{
			reader?.Dispose();
		}
	}

	private void WriteMaps()
	{
		foreach (var map in GameMaps)
		{
			map.WriteMap();
		}
	}
}
