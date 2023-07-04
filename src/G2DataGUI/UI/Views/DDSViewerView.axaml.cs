using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views;

public partial class DDSViewerView : UserControl
{
    public DDSViewerView()
    {
        DataContext = DDSViewerViewModel.Instance;
        InitializeComponent();
    }
}