﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.Common.Data.Errors;
using G2DataGUI.Common.Paths;
using G2DataGUI.UI.Common.Locale;

namespace G2DataGUI.Common.Data.Levelups;

public class Levelups : BaseContainer
{
	public static Levelups Instance { get; private set; } = new();
	public ObservableCollection<CharacterLevelups> GameLevelups { get; private set; } = new();
	public event EventHandler CollectionRefreshed;

	private Levelups()
	{
        _ = ReadLevelupsAsync();
	}

	public void Save() => WriteLevelups();

	public void Reload() => _ = ReadLevelupsAsync();

	private async Task ReadLevelupsAsync() =>
		await Task.Run(ReadLevelups).ConfigureAwait(false);

	private void ReadLevelups()
	{
		try
		{
			GameLevelups.Clear();
			using FileStream reader = File.Open(
				Version.Instance.RootDataDirectory + GamePaths.LevelupsPath,
				FileMode.Open,
				FileAccess.Read);
			using MemoryStream memReader = new();
			reader.CopyTo(memReader);
			memReader.Seek(0, SeekOrigin.Begin);
			int index = 0;
			while (memReader.Position < memReader.Length)
			{
				GameLevelups.Add(
					CharacterLevelups.ReadCharacterLevelups(
						memReader,
						LocaleManager.Instance[LocaleKeys.Characters][index++]));
			}

			CollectionRefreshed?.Invoke(this, EventArgs.Empty);
		}
		catch (Exception ex)
		{
			Errors.Errors.Instance.AddError(new Error("Levelups", ex.Message));
		}
	}

	private void WriteLevelups()
	{
		using FileStream writer = File.Open(
			Version.Instance.RootDataDirectory + GamePaths.LevelupsPath,
			FileMode.OpenOrCreate,
			FileAccess.Write);
		foreach (var levelups in GameLevelups)
		{
			levelups.WriteCharacterLevelups(writer);
		}
	}

	public void GenerateCSV()
	{
		using FileStream stream = File.Open(
			ProjectPaths.LevelupsCSVPath,
			FileMode.Create,
			FileAccess.Write);
		using StreamWriter writer = new(stream);
		using FileStream streamSum = File.Open(
			ProjectPaths.LevelupsSumCSVPath,
			FileMode.Create,
			FileAccess.Write);
		using StreamWriter writerSum = new(streamSum);
		writer.WriteLine(CharacterLevelups.CSVHeader);
		writerSum.WriteLine(CharacterLevelups.CSVHeader);
		foreach (var levelups in GameLevelups)
		{
			levelups.GenerateCSV(writer);
			levelups.GenerateSumCSV(writerSum);
		}
	}
}
