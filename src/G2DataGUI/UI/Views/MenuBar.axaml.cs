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
}