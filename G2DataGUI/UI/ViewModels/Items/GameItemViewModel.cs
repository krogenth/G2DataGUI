using G2DataGUI.Domain.Items;
using G2DataGUI.Domain.Moves;
using G2DataGUI.Repositories.Items;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Items;

/// <summary>
/// Named GameItemViewModel, not ItemViewModel — the latter is already the
/// generic base every other feature's item wrapper derives from
/// (ItemViewModel<TModel> in UI.Common), and this is itself an
/// ItemViewModel<Item>.
///
/// Equipment/Usable are nullable both here and on the domain model — null
/// means "this game item doesn't have that block" (not equippable / not
/// usable). Unlike the first pass, toggling a block on/off is supported
/// now: IsEquipmentEnabled/IsUsableEnabled below both create a fresh
/// Equipment()/Usable() (all-zero defaults — both classes have an implicit
/// parameterless constructor, no custom one exists) when turned on, and
/// null out the reference when turned off. Equipment/Usable properties
/// have a private setter (not the usual constructor-only assignment
/// pattern of every other nested ViewModel) specifically because of this —
/// they need to change after construction, not just be built once from
/// whatever the model had at load time.
/// </summary>
public sealed class GameItemViewModel(Item model, ItemRepository repository) : ItemViewModel<Item>(model, repository)
{
	public int Id => Model.Id;

	public int MaxNameLength => Item.MaxNameLength;
	public int MaxDescriptionLength => Item.MaxDescriptionLength;

	public string Name
	{
		get => Model.Name;
		set => SetModelValue(value, m => m.Name, (m, v) => m.Name = v);
	}

	public string Description
	{
		get => Model.Description;
		set => SetModelValue(value, m => m.Description, (m, v) => m.Description = v);
	}

	public ItemStatsViewModel Stats { get; } = new ItemStatsViewModel(model.Stats, repository);

	public EquipmentViewModel? Equipment { get; private set; } = model.Equipment is not null ? new EquipmentViewModel(model.Equipment, repository) : null;

	public UsableViewModel? Usable { get; private set; } = model.Usable is not null ? new UsableViewModel(model.Usable, repository) : null;

	public bool IsEquipmentEnabled
	{
		get => Equipment is not null;
		set
		{
			if (value == (Equipment is not null))
			{
				return;
			}

			Model.Equipment = value ? new Equipment() : null;
			Equipment = value ? new EquipmentViewModel(Model.Equipment!, Repository) : null;

			Repository.MarkDirty();
			OnPropertyChanged(nameof(Equipment));
			OnPropertyChanged(nameof(IsEquipmentEnabled));
		}
	}

	public bool IsUsableEnabled
	{
		get => Usable is not null;
		set
		{
			if (value == (Usable is not null))
			{
				return;
			}

			Model.Usable = value ? new Usable() : null;
			Usable = value ? new UsableViewModel(Model.Usable!, Repository) : null;

			Repository.MarkDirty();
			OnPropertyChanged(nameof(Usable));
			OnPropertyChanged(nameof(IsUsableEnabled));
		}
	}
}
