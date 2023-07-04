using G2DataGUI.Common.Data.Items;

namespace G2DataGUI.UI.ViewModels;

public class ItemEquipmentViewModel : BaseViewModel
{
    private Equipment _selectedItemEquipment = new Equipment();
    private bool _hasEquipment = false;

    public static ItemEquipmentViewModel Instance { get; } = new();

    public Equipment SelectedItemEquipment
    {
        get => _selectedItemEquipment;
        set
        {
            if (value == null) _selectedItemEquipment = new Equipment();
            else _selectedItemEquipment = value;
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