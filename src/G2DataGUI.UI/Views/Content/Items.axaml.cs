using Avalonia.Controls;

namespace G2DataGUI.UI.Views.Content;

public partial class Items : UserControl
{
	public Items()
	{
		InitializeComponent();
		//this.Find<CheckBox>("HasEquipment").Click += ItemsViewModel.Instance.HasEquipment_Click;
		//this.Find<CheckBox>("HasUsable").Click += ItemsViewModel.Instance.HasUsable_Click;
	}
}