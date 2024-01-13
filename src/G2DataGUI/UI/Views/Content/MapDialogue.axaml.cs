using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class MapDialogue : UserControl
{
    public MapDialogue()
    {
		DataContext = MapDialogueViewModel.Instance;
        InitializeComponent();
    }
}
