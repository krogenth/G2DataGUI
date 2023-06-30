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
    private Equipment _selectedItemEquipment = new Equipment();
    private Usable _selectedItemUsable = new Usable();

    public static ItemsViewModel Instance { get; } = new();

    private ItemsViewModel()
    {
        _selectedItemItem = GameItems.First();
        if (_selectedItemItem.EquipmentObject != null) SelectedItemEquipment = _selectedItemItem.EquipmentObject;
        if (_selectedItemItem.UsableObject != null) SelectedItemUsable = _selectedItemItem.UsableObject;
        Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
    }

    private void ItemsCollectionRefreshed(object sender, EventArgs _)
    {
        SelectedItemItem = GameItems[SelectedItemIndex];
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
            if (_selectedItemItem.EquipmentObject != null) SelectedItemEquipment = _selectedItemItem.EquipmentObject;
            if (_selectedItemItem.UsableObject != null) SelectedItemUsable = _selectedItemItem.UsableObject;
            OnPropertyChanged(nameof(SelectedItemItem));
        }
    }

    public void HasEquipment_Click(object sender, EventArgs e)
    {
        SelectedItemEquipment = (((CheckBox)sender).IsChecked == true && SelectedItemItem.EquipmentObject != null) ?
            SelectedItemItem.EquipmentObject :
            null;
    }

    public void HasUsable_Click(object sender, EventArgs e)
    {
        SelectedItemUsable = (((CheckBox)sender).IsChecked == true && SelectedItemItem.UsableObject != null) ?
            SelectedItemItem.UsableObject :
            null;
    }

    public Equipment SelectedItemEquipment
    {
        get => _selectedItemEquipment;
        set
        {
            if (value == null) _selectedItemEquipment = new Equipment();
            else _selectedItemEquipment = value;
            OnPropertyChanged(nameof(SelectedItemEquipment));
        }
    }
    public Usable SelectedItemUsable {
        get => _selectedItemUsable;
        set
        {
            if (value == null) _selectedItemUsable = new Usable();
            else _selectedItemUsable = value;
            OnPropertyChanged(nameof(SelectedItemUsable));
        }
    }
}