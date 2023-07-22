using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class MapEntries : UserControl
{
    public MapEntries()
    {
		DataContext = MapEntriesViewModel.Instance;
        InitializeComponent();
    }
}
