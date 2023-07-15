using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public sealed partial class Maps : UserControl
{
    public Maps()
    {
        DataContext = MapsViewModel.Instance;
        InitializeComponent();
    }
}
