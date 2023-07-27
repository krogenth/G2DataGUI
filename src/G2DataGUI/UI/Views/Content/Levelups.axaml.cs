using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class Levelups : UserControl
{
    public Levelups()
    {
        DataContext = LevelupsViewModel.Instance;
        InitializeComponent();
    }
}