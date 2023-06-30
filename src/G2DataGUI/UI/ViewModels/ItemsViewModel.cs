using Avalonia.Controls;
using G2DataGUI.Common.Data.Items;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace G2DataGUI.UI.ViewModels;

public sealed class ItemsViewModel : BaseViewModel
{
    public ObservableCollection<Item> GameItems { get; private set; } = Items.Instance.GetItems();
    private int _selectedItemIndex = 0;
    private Item _selectedItemItem;
    private ItemEquipmentViewModel _itemEquipmentViewModel = ItemEquipmentViewModel.Instance;
    private ItemUsableViewModel _itemUsableViewModel = ItemUsableViewModel.Instance;

    public static ItemsViewModel Instance { get; } = new();

    private ItemsViewModel()
    {
        _selectedItemItem = GameItems.First();
        _itemEquipmentViewModel.SelectedItemEquipment = _selectedItemItem.Equipment;
        _itemUsableViewModel.SelectedItemUsable = _selectedItemItem.Usable;
        Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
    }

    private void ItemsCollectionRefreshed(object sender, EventArgs _)
    {
        SelectedItemItem = GameItems[SelectedItemIndex];
        _itemEquipmentViewModel.SelectedItemEquipment = GameItems[SelectedItemIndex].Equipment;
        _itemUsableViewModel.SelectedItemUsable = GameItems[SelectedItemIndex].Usable;
    }

    public int SelectedItemIndex
    {
        get => _selectedItemIndex;
        set
        {
            if (value < 0) value = 0;
            if (value >= GameItems.Count) return;
            _selectedItemIndex = value;
            SelectedItemItem = GameItems.ElementAt(value);
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