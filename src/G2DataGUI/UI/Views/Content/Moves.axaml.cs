using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public sealed partial class Moves : UserControl
{
    public Moves()
    {
        DataContext = MovesViewModel.Instance;
        InitializeComponent();
    }
}