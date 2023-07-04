using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class EnemyAISection : UserControl
{
    public EnemyAISection()
    {
        DataContext = EnemyAISectionViewModel.Instance;
        InitializeComponent();
    }
}