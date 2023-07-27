using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class Skillbooks : UserControl
{
    public Skillbooks()
    {
        DataContext = SkillbooksViewModel.Instance;
        InitializeComponent();
    }
}