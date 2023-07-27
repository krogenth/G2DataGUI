using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class BossStats : UserControl
{
    public BossStats()
    {
		DataContext = BossStatsViewModel.Instance;
        InitializeComponent();
    }
}
