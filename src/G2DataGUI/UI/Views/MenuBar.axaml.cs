using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using G2DataGUI.UI.ViewModels;
using G2DataGUI.UI.Windows;

namespace G2DataGUI.UI.Views;

public sealed partial class MenuBar : UserControl
{
    public MenuBar()
    {
        DataContext = MenuBarViewModel.Instance;
        InitializeComponent();
    }

    /*protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        if (VisualRoot is MainWindow window)
        {
            ;
            MenuBarViewModel.Instance.Window = window;
            MenuBarViewModel.Instance.StyleThemeChanged += window.OnStyleThemeChange;
        }
    }*/
}