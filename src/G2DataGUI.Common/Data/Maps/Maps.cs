using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Data.Errors;
using G2DataGUI.Common.Data.Models;

namespace G2DataGUI.Common.Data.Maps;

public class Maps
{
    public static Maps Instance { get; private set; } = new Maps();
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
		try
		{
			GameMaps.Clear();
			foreach (var file in Directory.GetFiles(Version.Instance.RootDataDirectory + "map", "*.mdt", SearchOption.AllDirectories))
			{
				using FileStream reader = File.Open(file, FileMode.Open);
				GameMaps.Add(Map.ReadMap(reader, file));
			}

			CollectionRefreshed?.Invoke(this, EventArgs.Empty);

			using var modelFile = File.Open(
					"F:\\projects\\G2DataGUI\\build\\Debug\\net7.0\\models\\2a00.chr_model_1.nj",
					FileMode.Open,
					FileAccess.Read);
			NJCM model = NJCM.ReadNJCM(modelFile, 0);
		}
		catch (Exception ex)
		{
			Errors.Errors.Instance.AddError(new Error("Maps", ex.Message));
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
