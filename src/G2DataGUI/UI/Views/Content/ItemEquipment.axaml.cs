using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class ItemEquipment : UserControl
{
    public ItemEquipment()
    {
        DataContext = ItemEquipmentViewModel.Instance;
        InitializeComponent();
    }
}