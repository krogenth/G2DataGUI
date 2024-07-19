using System.Collections.ObjectModel;

namespace G2DataGUI.Common.Data.Audio;

public class AudioNode(string title, string path, bool isFile, ObservableCollection<AudioNode> children)
{
	public ObservableCollection<AudioNode> Children { get; } = children;
	public string Title { get; } = title;
	public string Path { get; } = path;
	public bool IsFile { get; } = isFile;

	public AudioNode(string title, string path, bool isFile) : this(title, path, isFile, []) { }
}
