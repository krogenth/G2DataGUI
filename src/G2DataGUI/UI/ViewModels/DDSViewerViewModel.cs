using G2DataGUI.Common.Data.DDS;
using G2DataGUI.Events;
using G2DataGUI.IO.DDS;
using Pfim;
using System;
using System.Collections.ObjectModel;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.Common.Extensions;

namespace G2DataGUI.UI.ViewModels;

public class DDSViewerViewModel : BaseViewModel
{
    public ObservableCollection<DDSNode> GameDirectoryDDSFiles { get; private set; } = DDSFiles.Instance.DirectoryDDSFiles;
    private int _imageWidth = 0;
    private int _imageHeight = 0;
    private DDSNode _selectedDDSFile = null;
    private IImage _image = null;
    public int TreeViewWidth { get; } = 200;
    private readonly int _minimumImageWidth = 700;
    private readonly int _minimumViewWidth = 900;
    public event EventHandler<ImageEventArgs> ImageChanged;

    public static DDSViewerViewModel Instance { get; private set; } = new();

    private DDSViewerViewModel()
    {
        DDSFiles.Instance.CollectionRefreshed += DDSFileCollectionRefreshed;
    }

	private void DDSFileCollectionRefreshed(object sender, EventArgs args) => Image = null;

	private void LoadDDSFile() =>
        Image = SelectedDDSFile == null || !SelectedDDSFile.IsFile ? null : DDSLoader.LoadDDSFile(SelectedDDSFile.Path);

	public int ImageWidth
    {
        get => _imageWidth;
        set
        {
            _imageWidth = value;
            OnPropertyChanged(nameof(ImageWidth));
            OnPropertyChanged(nameof(ViewWidth));
            OnPropertyChanged(nameof(ImagePaneWidth));
        }
    }

    public int ImageHeight
    {
        get => _imageHeight;
        set
        {
            _imageHeight = value;
            OnPropertyChanged(nameof(ImageHeight));
        }
    }

    public DDSNode SelectedDDSFile
    {
        get => _selectedDDSFile;
        set
        {
            if (value.IsFile)
            {
                _selectedDDSFile = value;
                LoadDDSFile();
                OnPropertyChanged(nameof(SelectedDDSFile));
            }
        }
    }

    public IImage Image
    {
        get => _image;
        set
        {
            _image = value;
            ImageWidth = value != null ? value.Width : 0;
            ImageHeight = value != null ? value.Height : 0;
            OnPropertyChanged(nameof(Image));
            OnPropertyChanged(nameof(ShowInformation));
            OnPropertyChanged(nameof(ImageGUID));
            ImageChanged?.Invoke(this, new ImageEventArgs(value));
        }
    }

    public uint ImageGUID
    {
        get => Image?.GUID() ?? 0;
    }

    public bool ShowInformation { get => Image != null; }

    public int ViewWidth
    {
        get => 16 + (ImageWidth > _minimumImageWidth ? ImageWidth + TreeViewWidth : _minimumViewWidth);
    }

    public int ImagePaneWidth
    {
        get => 16 + (ImageWidth > _minimumImageWidth ? ImageWidth : _minimumImageWidth);
    }
}
