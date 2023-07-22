using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class MapShop : UserControl
{
    public MapShop()
    {
		DataContext = MapShopViewModel.Instance;
        InitializeComponent();
    }
}
