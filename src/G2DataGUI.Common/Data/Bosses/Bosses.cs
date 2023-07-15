using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Paths;

namespace G2DataGUI.Common.Data.Bosses;

public class Bosses
{
	public static Bosses Instance { get; } = new();
	public ObservableCollection<Boss> GameBosses = new();
	public event EventHandler CollectionRefreshed;

	private Bosses()
    {
        ReadBosses();
		DifficultyMode.Instance.DifficultyChanged += OnDifficultyChange;
    }

	public void Save() => WriteBosses();

	public void Reload() => ReadBosses();

	private void OnDifficultyChange(object sender, EventArgs eventArgs) => ReadBosses();

    private async Task ReadBosses()
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
		using FileStream stream = File.Open(ProjectPaths.EnemiesCSVFile, FileMode.Create, FileAccess.Write);
		using StreamWriter writer = new(stream);
	}

	private void GenerateBossMovesCSV()
	{
		using FileStream stream = File.Open(ProjectPaths.EnemyMovesCSVFile, FileMode.Create, FileAccess.Write);
		using StreamWriter writer = new(stream);
	}
}
