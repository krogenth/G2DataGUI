using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class Items : UserControl
{
    public Items()
    {
        DataContext = ItemsViewModel.Instance;
        InitializeComponent();
        this.Find<CheckBox>("HasEquipment").Click += ItemsViewModel.Instance.HasEquipment_Click;
        this.Find<CheckBox>("HasUsable").Click += ItemsViewModel.Instance.HasUsable_Click;
    }
}