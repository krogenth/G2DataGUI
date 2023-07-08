using G2DataGUI.Events;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class DDSViewerWindowViewModel : BaseViewModel
{
    private int _windowWidth;

    public static DDSViewerWindowViewModel Instance { get; private set; } = new();

    private DDSViewerWindowViewModel()
    {
        DDSViewerViewModel.Instance.ImageChanged += OnImageChange;
        CalculateWindowWidth();
    }

	public void OnImageChange(object sender, ImageEventArgs e) => CalculateWindowWidth();

	private void CalculateWindowWidth() => WindowWidth = DDSViewerViewModel.Instance.ViewWidth;

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