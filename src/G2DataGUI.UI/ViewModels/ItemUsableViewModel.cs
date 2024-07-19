using G2DataGUI.Common.Data.Items;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class ItemUsableViewModel(UsableStats usable) : BaseViewModel
{
	public UsableStats SelectedItemUsable { get; set; } = usable;
}
