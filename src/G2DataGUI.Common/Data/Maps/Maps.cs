using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace G2DataGUI.Common.Data.Maps;

public class Maps
{
    public static Maps Instance { get; private set; } = new Maps();
    public ObservableCollection<Map> GameMaps { get; private set; } = new();
    public event EventHandler CollectionRefreshed;

    private Maps()
    {
		ReadMapsAsync();
    }

	public void Save() => WriteMaps();

	public void Reload() => ReadMapsAsync();

	private async Task ReadMapsAsync() =>
		await Task.Run(ReadMaps).ConfigureAwait(false);

    private void ReadMaps()
    {
		GameMaps.Clear();
        foreach(var file in Directory.GetFiles(Version.Instance.RootDataDirectory + "map", "*.mdt", SearchOption.AllDirectories))
        {
            using FileStream reader = File.Open(file, FileMode.Open);
            Map map = Map.ReadMap(reader, file);
            if (map != null)
            {
				GameMaps.Add(map);
            }
        }

        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

	private void WriteMaps()
	{
		foreach (var map in GameMaps)
		{
			map.WriteMap();
		}
	}
}
