using Avalonia.Controls;
using G2DataGUI.Common.Data.Items;
using System.Collections.ObjectModel;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public sealed class ItemsViewModel : BaseViewModel
{
	public ObservableCollection<Item> GameItems { get; set; } = Items.Instance.GameItems;
	private int _selectedItemIndex = 0;
	public Item? SelectedItemItem { get; set; }
	public ItemEquipmentViewModel SelectedItemEquipmentViewModel { get; set; } = new(new());
	public ItemUsableViewModel SelectedItemUsableViewModel { get; set; } = new(new());
	public bool HasEquipment { get; set; } = false;
	public bool HasUsable { get; set; } = false;

	public ItemsViewModel()
	{
		SelectedItemIndex = 0;
		Items.Instance.CollectionRefreshed += ItemsCollectionRefreshed;
	}

	private void ItemsCollectionRefreshed(object? sender, EventArgs _) =>
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
			else if (value >= GameItems.Count)
			{
				value = GameItems.Count - 1;
			}

			if (GameItems.Count == 0)
			{
				return;
			}

			_selectedItemIndex = value;
			SelectedItemItem = GameItems[value];
			HasEquipment = SelectedItemItem.Equipment != null;
			HasUsable = SelectedItemItem.Usable != null;
			OnPropertyChanged(nameof(SelectedItemItem));
			OnPropertyChanged(nameof(SelectedItemEquipmentViewModel));
			OnPropertyChanged(nameof(HasEquipment));
			OnPropertyChanged(nameof(HasUsable));
		}
	}

	public void HasEquipment_Click(object? sender, EventArgs e)
	{
		SelectedItemEquipmentViewModel = new(((CheckBox)sender!).IsChecked!.Value ?
			SelectedItemItem?.Equipment ?? new EquipmentStats() :
			new EquipmentStats());
		OnPropertyChanged(nameof(HasEquipment));
	}

	public void HasUsable_Click(object? sender, EventArgs e)
	{
		SelectedItemUsableViewModel = new(((CheckBox)sender!).IsChecked!.Value ?
			SelectedItemItem?.Usable ?? new UsableStats() :
			new UsableStats());
		OnPropertyChanged(nameof(HasUsable));
	}
}
