using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class CharacterLevelups : UserControl
{
    public CharacterLevelups()
    {
        DataContext = CharacterLevelupsViewModel.Instance;
        InitializeComponent();
    }
}