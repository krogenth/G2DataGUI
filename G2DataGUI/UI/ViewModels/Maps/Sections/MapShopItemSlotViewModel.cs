using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Domain.Maps;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Items;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

/// <summary>
/// One of a MapShopSection's 12 item slots. Not built on ItemViewModel's
/// SetModelValue — that helper takes a model-level getter/setter, but
/// this wraps one ARRAY INDEX (MapShopItemList.ItemOffsets[index]), so it
/// mutates the array directly and calls MarkDirty/OnPropertyChanged
/// itself instead. SelectedItem is a plain pass-through to ItemOffset
/// (not its own backing field) — same reference-by-name pattern as every
/// other item picker in this project, just simpler since there's no
/// separate raw-offset property to keep in sync (ItemOffset itself is
/// the raw offset).
/// </summary>
public sealed class MapShopItemSlotViewModel(MapShopItemList model, int index, IRepository repository, ObservableCollection<GameItemViewModel> availableItems)
	: BaseViewModel
{
	public ObservableCollection<GameItemViewModel> AvailableItems => availableItems;

	public ushort ItemOffset
	{
		get => model.ItemOffsets[index];
		set
		{
			if (model.ItemOffsets[index] == value)
			{
				return;
			}

			model.ItemOffsets[index] = value;
			repository.MarkDirty();
			OnPropertyChanged(nameof(ItemOffset));
			OnPropertyChanged(nameof(SelectedItem));
		}
	}

	public GameItemViewModel? SelectedItem
	{
		get => AvailableItems.FirstOrDefault(item => item.Id == ItemOffset);
		set
		{
			if (value is not null)
			{
				ItemOffset = (ushort)value.Id;
			}
		}
	}
}
