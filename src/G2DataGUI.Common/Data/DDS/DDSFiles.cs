using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace G2DataGUI.Common.Data.DDS;

public class DDSFiles
{
    public static DDSFiles Instance { get; private set; } = new();
    public ObservableCollection<DDSNode> DirectoryDDSFiles { get; private set; } = new();
    public event EventHandler CollectionRefreshed;

    private DDSFiles()
    {
        _ = ReadDDSFileSystemStructureAsync();
    }

	public void Reload() => _ = ReadDDSFileSystemStructureAsync();

	private async Task ReadDDSFileSystemStructureAsync() =>
		await Task.Run(() => ReadDDSFileSystemStructure()).ConfigureAwait(false);

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
            var child = new DDSNode(new DirectoryInfo(dir).Name, dir, 0, false, RecursiveDirectoryCrawl(dir));
            node.Add(child);
        }

        foreach (string file in Directory.GetFiles(directory, "*.dds"))
        {
			//using FileStream stream = File.Open(file, FileMode.Open, FileAccess.Read);
			//var guid = DDSLoader.GetGuid(stream);

			DDSNode child = new(new DirectoryInfo(file).Name, file, 0, true);
            node.Add(child);
		}

        return node;
    }
}
