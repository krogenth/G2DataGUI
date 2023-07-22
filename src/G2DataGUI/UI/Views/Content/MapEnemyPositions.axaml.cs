using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class MapEnemyPositions : UserControl
{
    public MapEnemyPositions()
    {
		DataContext = MapEnemyPositionsViewModel.Instance;
        InitializeComponent();
    }
}
