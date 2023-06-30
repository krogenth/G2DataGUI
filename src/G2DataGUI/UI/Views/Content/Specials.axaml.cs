using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class Specials : UserControl
{
    public Specials()
    {
        DataContext = SpecialsViewModel.Instance;
        InitializeComponent();
    }
}