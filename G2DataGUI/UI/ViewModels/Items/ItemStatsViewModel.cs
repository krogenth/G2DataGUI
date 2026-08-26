using G2DataGUI.Domain.Items;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Items;

public sealed class ItemStatsViewModel(ItemStats model, IRepository repository)
	: ItemViewModel<ItemStats>(model, repository)
{
	public byte EntryType
	{
		get => Model.EntryType;
		set => SetModelValue(value, m => m.EntryType, (m, v) => m.EntryType = v);
	}

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}

	public byte Unknown2
	{
		get => Model.Unknown2;
		set => SetModelValue(value, m => m.Unknown2, (m, v) => m.Unknown2 = v);
	}

	public byte Unknown3
	{
		get => Model.Unknown3;
		set => SetModelValue(value, m => m.Unknown3, (m, v) => m.Unknown3 = v);
	}

	public byte Icon
	{
		get => Model.Icon;
		set => SetModelValue(value, m => m.Icon, (m, v) => m.Icon = v);
	}

	public byte Unknown4
	{
		get => Model.Unknown4;
		set => SetModelValue(value, m => m.Unknown4, (m, v) => m.Unknown4 = v);
	}

	public int Price
	{
		get => Model.Price;
		set => SetModelValue(value, m => m.Price, (m, v) => m.Price = v);
	}
}
