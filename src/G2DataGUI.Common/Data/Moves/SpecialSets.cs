using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class SpecialSets
{
    public static SpecialSets Instance { get; } = new();
    private ObservableCollection<SpecialSet> _specialSets = new();
    public event EventHandler CollectionRefreshed;

    private SpecialSets()
    {
        ReadSpecialSets();
    }

    public void Save()
    {
        WriteSpecialSets();
    }

    public void Reload()
    {
        ReadSpecialSets();
    }

    private void ReadSpecialSets()
    {
        _specialSets.Clear();
        using (FileStream reader = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.Specials, FileMode.Open))
        using (MemoryStream memReader = new MemoryStream())
        {
            reader.CopyTo(memReader);
            memReader.Seek(0, SeekOrigin.Begin);
            while (memReader.Position < memReader.Length)
            {
                SpecialSet specialset = SpecialSet.ReadSpecialSet(memReader);
                if (specialset != null) _specialSets.Add(specialset);
            }
        }
        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    private void WriteSpecialSets()
    {
        using (FileStream writer = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.Specials, FileMode.OpenOrCreate))
        {
            foreach (SpecialSet specialSet in _specialSets)
            {
                specialSet.WriteSpecialSet(writer);
            }
        }
    }

    public ObservableCollection<SpecialSet> GetSpecialsSets() { return _specialSets; }
}