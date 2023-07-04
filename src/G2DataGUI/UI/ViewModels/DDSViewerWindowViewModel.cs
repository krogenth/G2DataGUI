using G2DataGUI.Events;

namespace G2DataGUI.UI.ViewModels;

public class DDSViewerWindowViewModel : BaseViewModel
{
    private int _windowWidth;

    public static DDSViewerWindowViewModel Instance { get; private set; } = new();

    private DDSViewerWindowViewModel()
    {
        DDSViewerViewModel.Instance.ImageChanged += OnImageChange;
        CalculateWindowWidth(DDSViewerViewModel.Instance.Image.Width);
    }

    public void OnImageChange(object sender, ImageEventArgs e)
    {
        CalculateWindowWidth(e.Image.Width);
    }

    private void CalculateWindowWidth(int imageWidth)
    {
        WindowWidth = DDSViewerViewModel.Instance.ViewWidth;
    }

    public int WindowWidth
    {
        get => _windowWidth;
        set
        {
            _windowWidth = value;
            OnPropertyChanged(nameof(WindowWidth));
        }
    }
}
