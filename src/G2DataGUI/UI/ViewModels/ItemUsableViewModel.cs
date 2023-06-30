using G2DataGUI.Common.Data.Items;

namespace G2DataGUI.UI.ViewModels;

public class ItemUsableViewModel : BaseViewModel
{
    private Usable _selectedItemUsable = new Usable();
    private bool _hasUsable = false;

    public static ItemUsableViewModel Instance { get; } = new();

    public Usable SelectedItemUsable
    {
        get => _selectedItemUsable;
        set
        {
            if (value == null) _selectedItemUsable = new Usable();
            else _selectedItemUsable = value;
            HasUsable = value != null;
            OnPropertyChanged(nameof(SelectedItemUsable));
        }
    }

    public bool HasUsable { get => _hasUsable; set { _hasUsable = value; OnPropertyChanged(nameof(HasUsable)); } }
}