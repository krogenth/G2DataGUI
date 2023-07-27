using Avalonia;
using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Windows;

public partial class DDSViewerWindow : Window
{
    public DDSViewerWindow()
    {
        DataContext = DDSViewerWindowViewModel.Instance;
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }
}