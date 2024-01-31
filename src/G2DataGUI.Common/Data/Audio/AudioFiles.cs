using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;

namespace G2DataGUI.Common.Data.Audio;

public class AudioFiles
{
	public static AudioFiles Instance { get; private set; } = new();
	public ObservableCollection<AudioNode> DirectoryAudioFiles { get; private set; } = [];
	public event EventHandler CollectionRefreshed;

	private AudioFiles()
	{
		_ = ReadAudioFileSystemStructureAsync();
	}

	public void Reload() { }

	private async Task ReadAudioFileSystemStructureAsync() =>
		await Task.Run(ReadAudioFileSystemStructure).ConfigureAwait(false);

	private void ReadAudioFileSystemStructure()
	{
		DirectoryAudioFiles.Clear();
		foreach (var node in RecursiveDirectoryCrawl(Version.Instance.RootContentDirectory))
		{
			DirectoryAudioFiles.Add(node);
		}

		CollectionRefreshed?.Invoke(this, EventArgs.Empty);
	}

	private ObservableCollection<AudioNode> RecursiveDirectoryCrawl(string directory)
	{
		ObservableCollection<AudioNode> node = [];

		foreach(string dir in Directory.GetDirectories(directory))
		{
			var child = new AudioNode(new DirectoryInfo(dir).Name, dir, false, RecursiveDirectoryCrawl(dir));
			node.Add(child);
		}

		foreach(string file in Directory.GetFiles(directory, "*.ogg"))
		{
			AudioNode child = new(new DirectoryInfo(file).Name, file, true);
			node.Add(child);
		}

		return node;
	}
}
