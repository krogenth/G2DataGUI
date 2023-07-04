using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace G2DataGUI.UI.Windows;

public partial class DDSViewerWindow : Window
{
    public DDSViewerWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
    }
}