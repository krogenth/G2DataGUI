using G2DataGUI.Common.Data.DDS;
using G2DataGUI.IO.DDS;
using Pfim;
using System;
using System.Collections.ObjectModel;

namespace G2DataGUI.UI.ViewModels;

public class DDSViewerViewModel : BaseViewModel
{
    public ObservableCollection<DDSFile> GameDDSFiles { get; private set; } = DDSFiles.Instance.GetDDSFiles();
    private int _imageWidth = 0;
    private int _imageHeight = 0;
    private int _selectedDDSFileIndex = 0;
    private IImage _image = null;

    public static DDSViewerViewModel Instance { get; private set; } = new();

    private DDSViewerViewModel()
    {
        DDSFiles.Instance.CollectionRefreshed += DDSFileCollectionRefreshed;
        if (GameDDSFiles.Count > 0)
        {
            LoadDDSFile();
        }
    }

    private void DDSFileCollectionRefreshed(object sender, EventArgs args)
    {
        if (GameDDSFiles.Count > 0)
        {
            LoadDDSFile();
        }
    }

    private void LoadDDSFile()
    {
        Image = DDSLoader.LoadDDSFile(GameDDSFiles[SelectedDDSFileIndex].Path);
        ImageHeight = Image.Height;
        ImageWidth = Image.Width;
    }

    public int ImageWidth
    {
        get => _imageWidth;
        set
        {
            _imageWidth = value;
            OnPropertyChanged(nameof(ImageWidth));
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

    public int SelectedDDSFileIndex
    {
        get => _selectedDDSFileIndex;
        set
        {
            _selectedDDSFileIndex = value;
            LoadDDSFile();
            OnPropertyChanged(nameof(SelectedDDSFileIndex));
        }
    }

    public IImage Image
    {
        get => _image;
        set
        {
            _image = value;
            OnPropertyChanged(nameof(Image));
        }
    }
}