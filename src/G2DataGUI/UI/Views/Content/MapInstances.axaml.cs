using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class MapInstances : UserControl
{
    public MapInstances()
    {
		DataContext = MapInstancesViewModel.Instance;
        InitializeComponent();
    }
}
