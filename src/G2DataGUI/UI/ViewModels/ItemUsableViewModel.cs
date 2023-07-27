using G2DataGUI.Common.Data.Items;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class ItemUsableViewModel : BaseViewModel
{
    private Usable _selectedItemUsable = new();
    private bool _hasUsable = false;

    public static ItemUsableViewModel Instance { get; } = new();

	private ItemUsableViewModel() { }

    public Usable SelectedItemUsable
    {
        get => _selectedItemUsable;
        set
        {
            _selectedItemUsable = value == null ? new Usable() : value;
			HasUsable = value != null;
            OnPropertyChanged(nameof(SelectedItemUsable));
        }
    }

    public bool HasUsable
    {
        get => _hasUsable;
        set
        {
            _hasUsable = value;
            OnPropertyChanged(nameof(HasUsable));
        }
    }
}
