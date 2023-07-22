using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class MapEnemyGroups : UserControl
{
    public MapEnemyGroups()
    {
		DataContext = MapEnemyGroupsViewModel.Instance;
        InitializeComponent();
    }
}
