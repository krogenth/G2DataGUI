using System.Collections.ObjectModel;

namespace G2DataGUI.Common.Data.DDS;

public class DDSNode
{
    public ObservableCollection<DDSNode> Children { get; }
    public string Title { get; }
    public string Path { get; }
    public bool IsFile { get; }

    public DDSNode(string title, string path, bool isFile) : this(title, path, isFile, new ObservableCollection<DDSNode>()) { }

    public DDSNode(string title, string path, bool isFile, ObservableCollection<DDSNode> children)
    {
        Title = title;
        Path = path;
        IsFile = isFile;
        Children = children;
    }

    public void AddChild(DDSNode child)
    {
        Children.Add(child);
    }
}
