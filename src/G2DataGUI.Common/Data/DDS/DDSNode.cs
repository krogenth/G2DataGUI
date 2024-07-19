using System.Collections.ObjectModel;

namespace G2DataGUI.Common.Data.DDS;

public class DDSNode(string title, string path, uint guid, bool isFile, ObservableCollection<DDSNode> children)
{
	public ObservableCollection<DDSNode> Children { get; } = children;
	public string Title { get; } = title;
	public string Path { get; } = path;
	public bool IsFile { get; } = isFile;
	public uint Guid { get; } = guid;

	public DDSNode(string title, string path, uint guid, bool isFile) : this(title, path, guid, isFile, []) { }

	public void AddChild(DDSNode child) => Children.Add(child);
}
