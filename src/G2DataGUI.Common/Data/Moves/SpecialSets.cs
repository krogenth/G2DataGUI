using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Paths;
using G2DataGUI.UI.Common.Locale;

namespace G2DataGUI.Common.Data.Moves;

public class SpecialSets
{
    public static SpecialSets Instance { get; } = new();
    public ObservableCollection<SpecialSet> GameSpecialSets { get; private set; } = new();
    public event EventHandler CollectionRefreshed;
    public static int NumberOfSpecialSets = 0x0E;

    private SpecialSets()
    {
		ReadSpecialSetsAsync();
    }

	public void Save() => WriteSpecialSets();

	public void Reload() => ReadSpecialSetsAsync();

	private async Task ReadSpecialSetsAsync() =>
		await Task.Run(ReadSpecialSets).ConfigureAwait(false);

	private void ReadSpecialSets()
    {
        GameSpecialSets.Clear();
        using (FileStream reader = File.Open(Version.Instance.RootDataDirectory + GamePaths.SpecialsPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream memReader = new())
        {
            reader.CopyTo(memReader);
            memReader.Seek(0, SeekOrigin.Begin);
            while (memReader.Position < memReader.Length)
            {
                SpecialSet specialset = SpecialSet.ReadSpecialSet(memReader);
                if (specialset != null)
                {
                    GameSpecialSets.Add(specialset);
                }
            }
        }

        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    private void WriteSpecialSets()
    {
        using FileStream writer = File.Open(Version.Instance.RootDataDirectory + GamePaths.SpecialsPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        foreach (SpecialSet specialSet in GameSpecialSets)
        {
            specialSet.WriteSpecialSet(writer);
        }
    }

    public void GenerateCSV()
    {
        using FileStream stream = File.Open(ProjectPaths.SpecialsCSVFile, FileMode.Create, FileAccess.Write);
        using StreamWriter writer = new(stream);
		writer.Write("Character,");
        writer.WriteLine(SpecialSet.CSVHeader);
        for (int index = 0; index < GameSpecialSets.Count; index++)
        {
            writer.Write($"{LocaleManager.Instance[LocaleKeys.SpecialChars][index]},");
            GameSpecialSets[index].GenerateCSV(writer);
        }
    }
}
