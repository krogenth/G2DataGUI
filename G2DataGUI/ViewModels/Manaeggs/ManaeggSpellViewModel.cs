using G2DataGUI.Domain.Moves;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Manaeggs;

public sealed class ManaeggSpellViewModel(ManaeggSpell model, IRepository repository)
	: ItemViewModel<ManaeggSpell>(model, repository)
{
	public byte SpellOffset
	{
		get => Model.SpellOffset;
		set => SetModelValue(value, m => m.SpellOffset, (m, v) => m.SpellOffset = v);
	}

	public byte StartingLevel
	{
		get => Model.StartingLevel;
		set => SetModelValue(value, m => m.StartingLevel, (m, v) => m.StartingLevel = v);
	}

	public byte RequiredLevel
	{
		get => Model.RequiredLevel;
		set => SetModelValue(value, m => m.RequiredLevel, (m, v) => m.RequiredLevel = v);
	}

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}
}
