using G2DataGUI.Domain.Items;
using G2DataGUI.Repositories.Items;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Items;

/// <summary>
/// Named GameItemViewModel, not ItemViewModel — the latter is already the
/// generic base every other feature's item wrapper derives from
/// (ItemViewModel&lt;TModel&gt; in UI.Common), and this is itself an
/// ItemViewModel&lt;Item&gt;. Equipment/Usable are nullable: null means "this
/// game item doesn't have that block" (not equippable / not usable), same
/// as on the domain model — toggling a block on or off entirely isn't
/// supported by this pass, only editing an existing one.
/// </summary>
public sealed class GameItemViewModel : ItemViewModel<Item>
{
	public GameItemViewModel(Item model, ItemRepository repository) : base(model, repository)
	{
		Stats = new ItemStatsViewModel(model.Stats, repository);
		Equipment = model.Equipment is not null ? new EquipmentViewModel(model.Equipment, repository) : null;
		Usable = model.Usable is not null ? new UsableViewModel(model.Usable, repository) : null;
	}

	public int Id => Model.Id;

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

	public ItemStatsViewModel Stats { get; }

	public EquipmentViewModel? Equipment { get; }

	public UsableViewModel? Usable { get; }
}
