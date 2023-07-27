using Avalonia.Controls;
using G2DataGUI.Common.Data.Items;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public sealed class ItemsViewModel : BaseViewModel
{
    public ObservableCollection<Item> GameItems { get; private set; } = Items.Instance.GameItems;
    private int _selectedItemIndex;
    private Item _selectedItemItem;
    private readonly ItemEquipmentViewModel _itemEquipmentViewModel = ItemEquipmentViewModel.Instance;
    private readonly ItemUsableViewModel _itemUsableViewModel = ItemUsableViewModel.Instance;

    public static ItemsViewModel Instance { get; } = new();

    private ItemsViewModel()
    {
		SelectedItemIndex = 0;
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
    }

	private void ItemsCollectionRefreshed(object sender, EventArgs _) =>
		SelectedItemIndex = SelectedItemIndex;

	public int SelectedItemIndex
    {
        get => _selectedItemIndex;
        set
        {
            if (value < 0)
			{
				value = 0;
			}

			if (value >= GameItems.Count)
			{
				return;
			}

			_selectedItemIndex = value;
            SelectedItemItem = GameItems[value];
            OnPropertyChanged(nameof(SelectedItemItem));
        }
    }

    public Item SelectedItemItem
    {
        get => _selectedItemItem;
        set
        {
            _selectedItemItem = value;
            _itemEquipmentViewModel.SelectedItemEquipment = value.Equipment;
            _itemUsableViewModel.SelectedItemUsable = value.Usable;
            OnPropertyChanged(nameof(SelectedItemItem));
        }
    }

    public void HasEquipment_Click(object sender, EventArgs e)
    {
        _itemEquipmentViewModel.SelectedItemEquipment = (((CheckBox)sender).IsChecked == true && SelectedItemItem.Equipment != null) ?
            SelectedItemItem.Equipment :
            null;
    }

    public void HasUsable_Click(object sender, EventArgs e)
    {
        _itemUsableViewModel.SelectedItemUsable = (((CheckBox)sender).IsChecked == true && SelectedItemItem.Usable != null) ?
            SelectedItemItem.Usable :
            null;
    }
}
