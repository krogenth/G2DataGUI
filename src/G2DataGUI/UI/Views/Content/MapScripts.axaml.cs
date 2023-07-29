using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class MapScripts : UserControl
{
    public MapScripts()
    {
		DataContext = MapScriptsViewModel.Instance;
        InitializeComponent();
    }
}
