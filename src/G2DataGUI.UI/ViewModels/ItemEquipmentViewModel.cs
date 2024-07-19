using G2DataGUI.Common.Data.Items;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class ItemEquipmentViewModel(EquipmentStats equipment) : BaseViewModel
{
	public EquipmentStats SelectedItemEquipment { get; set; } = equipment;
}
