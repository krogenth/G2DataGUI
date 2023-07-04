using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class Skillbooks
{
    public static Skillbooks Instance { get; } = new();
    public ObservableCollection<Skillbook> _skillbooks = new();
    public event EventHandler CollectionRefreshed;
    public static int NumberOfSkillbooks = 0x0A;

    private Skillbooks()
    {
        ReadSkillbooks();
    }

    public void Save()
    {
        WriteSkillbooks();
    }

    public void Reload()
    {
        ReadSkillbooks();
    }

    private void ReadSkillbooks()
    {
        _skillbooks.Clear();
        using (FileStream reader = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.SkillbooksPath, FileMode.Open, FileAccess.Read))
        using (MemoryStream memReader = new MemoryStream())
        {
            reader.CopyTo(memReader);
            memReader.Seek(0, SeekOrigin.Begin);
            while (memReader.Position < memReader.Length)
            {
                Skillbook skillbook = Skillbook.ReadSkillbook(memReader);
                if (skillbook != null) _skillbooks.Add(skillbook);
            }
        }
        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    private void WriteSkillbooks()
    {
        using (FileStream writer = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.SkillbooksPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            foreach (Skillbook skillbook in _skillbooks)
            {
                skillbook.WriteSkillbook(writer);
            }
        }
    }

    public ObservableCollection<Skillbook> GetSkillbooks() { return _skillbooks; }
}