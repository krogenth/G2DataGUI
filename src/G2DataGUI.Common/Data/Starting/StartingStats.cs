﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.Common.Paths;
using G2DataGUI.UI.Common.Locale;
using G2DataGUI.Common.Data.Errors;

namespace G2DataGUI.Common.Data.Starting;

public class StartingStats : BaseContainer
{
    public static StartingStats Instance { get; } = new();
    public ObservableCollection<CharacterStartingStats> GameStartingStats { get; private set; } = new();
    public event EventHandler CollectionRefreshed;

    private StartingStats()
    {
        _ = ReadStartingStatsAsync();
    }

    public void Save() => WriteStartingStats();

    public void Reload() => _ = ReadStartingStatsAsync();

    private async Task ReadStartingStatsAsync() =>
        await Task.Run(ReadStartingStats).ConfigureAwait(false);

    private void ReadStartingStats()
    {
		try
		{
			GameStartingStats.Clear();
			using FileStream reader = File.Open(
				Version.Instance.RootDataDirectory + GamePaths.InitialPath,
				FileMode.Open,
				FileAccess.Read);
			using MemoryStream memReader = new();
			reader.CopyTo(memReader);
			memReader.Seek(0, SeekOrigin.Begin);
			int index = 0;
			while (memReader.Position < memReader.Length)
			{
				GameStartingStats.Add(
					CharacterStartingStats.ReadCharacterStartingStats(
						memReader,
						LocaleManager.Instance[LocaleKeys.Characters][index++]));
			}

			CollectionRefreshed?.Invoke(this, EventArgs.Empty);
		}
        catch (Exception ex)
		{
			Errors.Errors.Instance.AddError(new Error("Starting Stats", ex.Message));
		}
    }

    private void WriteStartingStats()
    {
        using FileStream writer = File.Open(
            Version.Instance.RootDataDirectory + GamePaths.InitialPath,
            FileMode.Open,
            FileAccess.Write);
        foreach (var stats in GameStartingStats)
        {
            stats.WriteCharacterStartingStats(writer);
        }
    }

	public void GenerateCSV()
	{
		using FileStream stream = File.Open(
			ProjectPaths.InitialCSVPath,
			FileMode.Create,
			FileAccess.Write);
		using StreamWriter writer = new(stream);
		writer.WriteLine(CharacterStartingStats.CSVHeader);
		foreach (var startingStats in GameStartingStats)
		{
			startingStats.GenerateCSV(writer);
			writer.WriteLine();
		}
	}
}
