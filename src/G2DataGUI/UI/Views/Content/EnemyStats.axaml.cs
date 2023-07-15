using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class EnemyStats : UserControl
{
    public EnemyStats()
    {
		DataContext = EnemyStatsViewModel.Instance;
        InitializeComponent();
    }
}
