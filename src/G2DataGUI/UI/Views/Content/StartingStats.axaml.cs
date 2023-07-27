using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class StartingStats : UserControl
{
    public StartingStats()
    {
        DataContext = StartingStatsViewModel.Instance;
        InitializeComponent();
    }
}