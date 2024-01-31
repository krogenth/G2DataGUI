using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views;

public partial class AudioViewerView : UserControl
{
    public AudioViewerView()
    {
		DataContext = AudioViewerViewModel.Instance;
        InitializeComponent();
    }
}
