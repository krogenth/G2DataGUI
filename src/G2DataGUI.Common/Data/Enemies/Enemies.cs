using System;
using System.Collections.ObjectModel;
using System.IO;
using G2DataGUI.Common.Paths;

namespace G2DataGUI.Common.Data.Enemies;

public class Enemies
{
    public static Enemies Instance { get; } = new Enemies();
    public ObservableCollection<Enemy> GameEnemies = new();
    public event EventHandler CollectionRefreshed;

    private Enemies()
    {
        ReadEnemies();
		DifficultyMode.Instance.DifficultyChanged += OnDifficultyChange;
	}

	public void Save() => WriteEnemies();

	public void Reload() => ReadEnemies();

	private void OnDifficultyChange(object sender, EventArgs e) => ReadEnemies();

	private void ReadEnemies()
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
		using FileStream stream = File.Open(ProjectPaths.EnemiesCSVFile, FileMode.Create, FileAccess.Write);
		using StreamWriter writer = new(stream);
	}

	private void GenerateEnemyMovesCSV()
	{
		using FileStream stream = File.Open(ProjectPaths.EnemyMovesCSVFile, FileMode.Create, FileAccess.Write);
		using StreamWriter writer = new(stream);
	}
}
