using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.DDS;

public class DDSFiles
{
    public static DDSFiles Instance { get; private set; } = new();
    private ObservableCollection<DDSFile> _ddsFiles = new();
    public event EventHandler CollectionRefreshed;

    private DDSFiles()
    {
        ReadDDSFiles();
    }

    public void Reload()
    {
        ReadDDSFiles();
    }

    private void ReadDDSFiles()
    {
        _ddsFiles.Clear();
        foreach (string file in Directory.EnumerateFiles(Version.Instance.RootDataDirectory, "*.dds", SearchOption.AllDirectories))
        {
            _ddsFiles.Add(new DDSFile() { Name = file.Split("afs\\")[1], Path = file });
        }
        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    public ObservableCollection<DDSFile> GetDDSFiles() { return _ddsFiles; }
}