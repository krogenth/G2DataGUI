using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class MapHTAs : UserControl
{
    public MapHTAs()
    {
		DataContext = MapHTAsViewModel.Instance;
        InitializeComponent();
    }
}
