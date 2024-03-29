﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Data.Errors;
using G2DataGUI.Common.Paths;

namespace G2DataGUI.Common.Data.Bosses;

public class Bosses
{
	public static Bosses Instance { get; } = new();
	public ObservableCollection<Boss> GameBosses = new();
	public event EventHandler CollectionRefreshed;

	private Bosses()
    {
		_ = ReadBossesAsync();
		DifficultyMode.Instance.DifficultyChanged += OnDifficultyChange;
    }

	public void Save() => WriteBosses();

	public void Reload() => _ = ReadBossesAsync();

	private void OnDifficultyChange(object sender, EventArgs eventArgs) => _ = ReadBossesAsync();

	private async Task ReadBossesAsync() =>
		await Task.Run(ReadBosses).ConfigureAwait(false);

    private void ReadBosses()
    {
		try
		{
			GameBosses.Clear();
			var directory =
				DifficultyMode.Instance.IsHardMode ?
				Version.Instance.RootDataDirectory + GamePaths.BossHardmodeDirectory :
				Version.Instance.RootDataDirectory + GamePaths.BossDirectory;
			foreach (var file in Directory.GetFiles(directory, "*_0.dat", SearchOption.AllDirectories))
			{
				using FileStream reader = File.Open(file, FileMode.Open);
				GameBosses.Add(Boss.ReadBoss(reader, file));

				if (Boss.FileHasSecondBoss(reader))
				{
					GameBosses.Add(Boss.ReadBoss(reader, file, true));
				}
			}

			CollectionRefreshed?.Invoke(this, EventArgs.Empty);
		}
		catch (Exception ex)
		{
			Errors.Errors.Instance.AddError(new Error("Bosses", ex.Message));
		}
    }

	private void WriteBosses()
	{
		foreach (var boss in GameBosses)
		{
			boss.WriteBoss();
		}
	}

	public void GenerateCSV()
	{
		GenerateBossesCSV();
		GenerateBossMovesCSV();
	}

	private void GenerateBossesCSV()
	{
		using FileStream stream = File.Open(
			DifficultyMode.Instance.IsHardMode ?
			ProjectPaths.BossesHardmodeCSVPath :
			ProjectPaths.BossesCSVPath,
			FileMode.Create,
			FileAccess.Write);
		using StreamWriter writer = new(stream);
		writer.WriteLine(Boss.BossCSVHeader);
		foreach (var boss in GameBosses)
		{
			boss.GenerateBossCSV(writer);
		}
	}

	private void GenerateBossMovesCSV()
	{
		using FileStream stream = File.Open(
			DifficultyMode.Instance.IsHardMode ?
			ProjectPaths.BossHardmodeMovesCSVPath :
			ProjectPaths.BossMovesCSVPath,
			FileMode.Create,
			FileAccess.Write);
		using StreamWriter writer = new(stream);
		writer.WriteLine(Boss.MovesetCSVHeader);
		foreach (var boss in GameBosses)
		{
			boss.GenerateMovesetCSV(writer);
		}
	}
}
