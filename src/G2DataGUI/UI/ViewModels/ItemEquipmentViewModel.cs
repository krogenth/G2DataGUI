using G2DataGUI.Common.Data.Items;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class ItemEquipmentViewModel : BaseViewModel
{
    private Equipment _selectedItemEquipment = new();
    private bool _hasEquipment = false;

    public static ItemEquipmentViewModel Instance { get; } = new();

    public Equipment SelectedItemEquipment
    {
        get => _selectedItemEquipment;
        set
        {
            _selectedItemEquipment = value == null ? new Equipment() : value;
			HasEquipment = value != null;
            OnPropertyChanged(nameof(SelectedItemEquipment));
        }
    }

    public bool HasEquipment
    {
        get => _hasEquipment;
        set
        {
            _hasEquipment = value;
            OnPropertyChanged(nameof(HasEquipment));
        }
    }
}