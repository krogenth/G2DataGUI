using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class Skills : UserControl
{
    public Skills()
    {
        DataContext = SkillsViewModel.Instance;
        InitializeComponent();
    }
}