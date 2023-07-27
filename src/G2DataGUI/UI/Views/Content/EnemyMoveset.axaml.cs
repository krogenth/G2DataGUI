using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class EnemyMoveset : UserControl
{
    public EnemyMoveset()
    {
        DataContext = EnemyMovesetViewModel.Instance;
        InitializeComponent();
    }
}
