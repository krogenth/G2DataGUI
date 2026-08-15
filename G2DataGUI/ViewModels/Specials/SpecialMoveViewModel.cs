using G2DataGUI.Domain.Specials;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Specials;

public sealed class SpecialMoveViewModel(SpecialMove model, IRepository repository)
	: ItemViewModel<SpecialMove>(model, repository)
{
	public byte MoveOffset
	{
		get => Model.MoveOffset;
		set => SetModelValue(value, m => m.MoveOffset, (m, v) => m.MoveOffset = v);
	}

	public byte StartingLevel
	{
		get => Model.StartingLevel;
		set => SetModelValue(value, m => m.StartingLevel, (m, v) => m.StartingLevel = v);
	}

	public ushort StoryFlag
	{
		get => Model.StoryFlag;
		set => SetModelValue(value, m => m.StoryFlag, (m, v) => m.StoryFlag = v);
	}
}
