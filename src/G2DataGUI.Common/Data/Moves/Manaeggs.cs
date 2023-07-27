using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Paths;
using G2DataGUI.UI.Common.Locale;

namespace G2DataGUI.Common.Data.Moves;

public class Manaeggs
{
    public static Manaeggs Instance { get; } = new();
    public ObservableCollection<Manaegg> GameManaeggs { get; private set; } = new();
    public event EventHandler CollectionRefreshed;
    public static int NumberOfManaeggs = 0x0B;

    private Manaeggs()
    {
        _ = ReadManaeggsAsync();
    }

	public void Save() => WriteManaeggs();

	public void Reload() => _ = ReadManaeggsAsync();

	private async Task ReadManaeggsAsync() =>
		await Task.Run(ReadManaeggs).ConfigureAwait(false);

	private void ReadManaeggs()
    {
        GameManaeggs.Clear();
        using FileStream reader = File.Open(Version.Instance.RootDataDirectory + GamePaths.ManaeggsPath, FileMode.Open, FileAccess.Read);
        using MemoryStream memReader = new();
        reader.CopyTo(memReader);
        memReader.Seek(0, SeekOrigin.Begin);
        while (memReader.Position < memReader.Length)
        {
            GameManaeggs.Add(Manaegg.ReadManaegg(memReader));
        }

        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    private void WriteManaeggs()
    {
        using FileStream writer = File.Open(Version.Instance.RootDataDirectory + GamePaths.ManaeggsPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        foreach (Manaegg manaegg in GameManaeggs)
        {
            manaegg.WriteManaegg(writer);
        }
    }

    public void GenerateCSV()
    {
        using FileStream stream = File.Open(ProjectPaths.ManaeggsCSVFile, FileMode.Create, FileAccess.Write);
        using StreamWriter writer = new(stream);
		writer.Write("Manaegg,");
        writer.WriteLine(Manaegg.CSVHeader);
        for (int index = 0; index < GameManaeggs.Count; index++)
        {
            writer.Write($"{LocaleManager.Instance[LocaleKeys.Eggs][index]},");
            GameManaeggs[index].GenerateCSV(writer);
        }
    }
}
