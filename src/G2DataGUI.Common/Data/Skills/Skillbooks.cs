using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Paths;
using G2DataGUI.UI.Common.Locale;

namespace G2DataGUI.Common.Data.Skills;

public class Skillbooks
{
    public static Skillbooks Instance { get; } = new();
    public ObservableCollection<Skillbook> GameSkillbooks { get; private set; } = new();
    public event EventHandler CollectionRefreshed;
    public static int NumberOfSkillbooks = 0x0A;

    private Skillbooks()
    {
        ReadSkillbooks();
    }

	public void Save() => WriteSkillbooks();

	public void Reload() => ReadSkillbooks();

	private async Task ReadSkillbooks()
    {
        GameSkillbooks.Clear();
        using FileStream reader = File.Open(Version.Instance.RootDataDirectory + GamePaths.SkillbooksPath, FileMode.Open, FileAccess.Read);
        using MemoryStream memReader = new();
        reader.CopyTo(memReader);
        memReader.Seek(0, SeekOrigin.Begin);
        while (memReader.Position < memReader.Length)
        {
            Skillbook skillbook = Skillbook.ReadSkillbook(memReader);
            if (skillbook != null)
            {
                GameSkillbooks.Add(skillbook);
            }
        }

        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    private void WriteSkillbooks()
    {
        using FileStream writer = File.Open(Version.Instance.RootDataDirectory + GamePaths.SkillbooksPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        foreach (Skillbook skillbook in GameSkillbooks)
        {
            skillbook.WriteSkillbook(writer);
        }
    }

	public void GenerateCSV()
	{
		using FileStream stream = File.Open(ProjectPaths.SkillbooksCSVFile, FileMode.Create, FileAccess.Write);
		using StreamWriter writer = new(stream);
		writer.Write("Skillbook,");
		writer.WriteLine(Skillbook.CSVHeader);
		for (int index = 0; index < GameSkillbooks.Count; index++)
		{
			writer.Write($"{LocaleManager.Instance[LocaleKeys.Books][index]},");
			GameSkillbooks[index].GenerateCSV(writer);
		}
	}
}
