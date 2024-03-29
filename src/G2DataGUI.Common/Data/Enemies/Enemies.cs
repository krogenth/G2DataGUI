﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Data.Errors;
using G2DataGUI.Common.Paths;

namespace G2DataGUI.Common.Data.Enemies;

public class Enemies
{
    public static Enemies Instance { get; } = new Enemies();
    public ObservableCollection<Enemy> GameEnemies = new();
    public event EventHandler CollectionRefreshed;

    private Enemies()
    {
        _ = ReadEnemiesAsync();
		DifficultyMode.Instance.DifficultyChanged += OnDifficultyChange;
	}

	public void Save() => WriteEnemies();

	public void Reload() => _ = ReadEnemiesAsync();

	private void OnDifficultyChange(object sender, EventArgs e) => _ = ReadEnemiesAsync();

    private async Task ReadEnemiesAsync() =>
		await Task.Run(ReadEnemies).ConfigureAwait(false);

	private void ReadEnemies()
    {
		try
		{
			GameEnemies.Clear();
			var directory =
				DifficultyMode.Instance.IsHardMode ?
				Version.Instance.RootDataDirectory + GamePaths.EnemyHardmodeDirectory :
				Version.Instance.RootDataDirectory + GamePaths.EnemyDirectory;
			foreach (var file in Directory.GetFiles(directory, "*_0.dat", SearchOption.AllDirectories))
			{
				using FileStream reader = File.Open(file, FileMode.Open, FileAccess.Read);
				using MemoryStream memReader = new();
				reader.CopyTo(memReader);
				GameEnemies.Add(Enemy.ReadEnemy(memReader, file));

				if (Enemy.FileHasSecondEnemy(memReader))
				{
					GameEnemies.Add(Enemy.ReadEnemy(memReader, file, true));
				}
			}

			CollectionRefreshed?.Invoke(this, EventArgs.Empty);
		}
        catch (Exception ex)
		{
			Errors.Errors.Instance.AddError(new Error("Enemies", ex.Message));
		}
    }

    private void WriteEnemies()
    {
        foreach (var enemy in GameEnemies)
        {
            enemy.WriteEnemy();
        }
    }

	public void GenerateCSV()
	{
		GenerateEnemiesCSV();
		GenerateEnemyMovesCSV();
	}

	private void GenerateEnemiesCSV()
	{
		using FileStream stream = File.Open(
			DifficultyMode.Instance.IsHardMode ?
			ProjectPaths.EnemiesHardmodeCSVPath :
			ProjectPaths.EnemiesCSVPath,
			FileMode.Create,
			FileAccess.Write);
		using StreamWriter writer = new(stream);
		writer.WriteLine(Enemy.EnemyCSVHeader);
		foreach (var enemy in GameEnemies)
		{
			enemy.GenerateEnemyCSV(writer);
		}
	}

	private void GenerateEnemyMovesCSV()
	{
		using FileStream stream = File.Open(
			DifficultyMode.Instance.IsHardMode ?
			ProjectPaths.EnemyHardmodeMovesCSVPath :
			ProjectPaths.EnemyMovesCSVPath,
			FileMode.Create,
			FileAccess.Write);
		using StreamWriter writer = new(stream);
		writer.WriteLine(Enemy.MovesetCSVHeader);
		foreach (var enemy in GameEnemies)
		{
			enemy.GenerateMovesetCSV(writer);
		}
	}
}
