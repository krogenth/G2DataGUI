using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Domain.Specials;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Moves;

namespace G2DataGUI.UI.ViewModels.Specials;

/// <summary>Same "reference by name, not raw offset" pattern as
/// ManaeggSpellViewModel.SelectedMove — see its doc comment. AvailableMoves
/// is the same ObservableCollection MovesViewModel.Items already is.</summary>
public sealed class SpecialMoveViewModel(SpecialMove model, IRepository repository, ObservableCollection<MoveViewModel> availableMoves)
	: ItemViewModel<SpecialMove>(model, repository)
{
	public ObservableCollection<MoveViewModel> AvailableMoves => availableMoves;

	public MoveViewModel? SelectedMove
	{
		get => AvailableMoves.FirstOrDefault(move => move.Id == MoveOffset);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((byte)value.Id, m => m.MoveOffset, (m, v) => m.MoveOffset = v, nameof(MoveOffset));
			OnPropertyChanged(nameof(SelectedMove));
		}
	}

	public byte MoveOffset
	{
		get => Model.MoveOffset;
		set
		{
			SetModelValue(value, m => m.MoveOffset, (m, v) => m.MoveOffset = v);
			OnPropertyChanged(nameof(SelectedMove));
		}
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
