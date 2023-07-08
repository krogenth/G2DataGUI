using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.DDS;

public class DDSFiles
{
    public static DDSFiles Instance { get; private set; } = new();
    public ObservableCollection<DDSNode> DirectoryDDSFiles { get; private set; } = new();
    public event EventHandler CollectionRefreshed;

    private DDSFiles()
    {
        ReadDDSFileSystemStructure();
    }

    public void Reload()
    {
        ReadDDSFileSystemStructure();
    }

    private void ReadDDSFileSystemStructure()
    {
        DirectoryDDSFiles.Clear();
        foreach (var node in RecursiveDirectoryCrawl(Version.Instance.RootDataDirectory))
        {
            DirectoryDDSFiles.Add(node);
        }

        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    private ObservableCollection<DDSNode> RecursiveDirectoryCrawl(string directory)
    {
        ObservableCollection<DDSNode> node = new();

        foreach (string dir in Directory.GetDirectories(directory))
        {
            var child = new DDSNode(new DirectoryInfo(dir).Name, dir, false, RecursiveDirectoryCrawl(dir));
            node.Add(child);
        }

        foreach (string file in Directory.GetFiles(directory, "*.dds"))
        {
            var child = new DDSNode(new DirectoryInfo(file).Name, file, true);
            node.Add(child);
        }

        return node;
    }
}
