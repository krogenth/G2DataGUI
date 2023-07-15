using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class BossMovesets : UserControl
{
    public BossMovesets()
    {
		DataContext = BossMovesetsViewModel.Instance;
        InitializeComponent();
    }
}
