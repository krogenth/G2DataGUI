using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class MapMOSs : UserControl
{
    public MapMOSs()
    {
		DataContext = MapMOSsViewModel.Instance;
        InitializeComponent();
    }
}
