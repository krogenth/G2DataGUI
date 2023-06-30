using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class Manaeggs
{
    public static Manaeggs Instance { get; } = new();
    private ObservableCollection<Manaegg> _manaeggs = new();
    public event EventHandler CollectionRefreshed;

    private Manaeggs()
    {
        ReadManaeggs();
    }

    public void Save()
    {
        WriteManaeggs();
    }

    public void Reload()
    {
        ReadManaeggs();
    }

    private void ReadManaeggs()
    {
        _manaeggs.Clear();
        using (FileStream reader = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.Manaeggs, FileMode.Open))
        using (MemoryStream memReader = new MemoryStream())
        {
            reader.CopyTo(memReader);
            memReader.Seek(0, SeekOrigin.Begin);
            while (memReader.Position < memReader.Length)
            {
                Manaegg manaegg = Manaegg.ReadManaegg(memReader);
                if (manaegg != null) _manaeggs.Add(manaegg);
            }
        }
        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    private void WriteManaeggs()
    {
        using (FileStream writer = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.Manaeggs, FileMode.OpenOrCreate))
        {
            foreach (Manaegg manaegg in _manaeggs)
            {
                manaegg.WriteManaegg(writer);
            }
        }
    }

    public ObservableCollection<Manaegg> GetManaeggs() { return _manaeggs; }
}