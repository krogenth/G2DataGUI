using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Domain.Moves;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Moves;

namespace G2DataGUI.UI.ViewModels.Manaeggs;

/// <summary>
/// SpellOffset is a byte index into the Moves table (see ManaeggSpell's own
/// doc comment) — SelectedMove below is what lets a View reference it by
/// Move, not raw offset, the same way ManaeggSpellCsvExporter already
/// resolves it for the CSV export. AvailableMoves is the SAME
/// ObservableCollection MovesViewModel.Items already is (passed down
/// through ManaeggsViewModel/ManaeggViewModel, not a fresh copy) — so a
/// Move renamed on the Moves page shows the new name here immediately,
/// and the ComboBox stays correct if a move is ever added/removed, since
/// ObservableCollection's own change notifications flow straight through.
/// </summary>
public sealed class ManaeggSpellViewModel(ManaeggSpell model, IRepository repository, ObservableCollection<MoveViewModel> availableMoves, int slotNumber)
	: ItemViewModel<ManaeggSpell>(model, repository)
{
	public int SlotNumber { get; } = slotNumber;
	public string Title => $"Slot {SlotNumber}";
	public ObservableCollection<MoveViewModel> AvailableMoves => availableMoves;

	/// <summary>Null when SpellOffset doesn't resolve to any currently-loaded
	/// Move — same "can't find it" case ManaeggSpellCsvExporter falls back
	/// to "<unknown N>" for. SpellOffset (below) stays visible/
	/// editable in the View either way, so that case isn't just an empty
	/// dropdown with no explanation or way to fix it.</summary>
	public MoveViewModel? SelectedMove
	{
		get => AvailableMoves.FirstOrDefault(move => move.Id == SpellOffset);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((byte)value.Id, m => m.SpellOffset, (m, v) => m.SpellOffset = v, nameof(SpellOffset));
			OnPropertyChanged(nameof(SelectedMove));
		}
	}

	public byte SpellOffset
	{
		get => Model.SpellOffset;
		set
		{
			SetModelValue(value, m => m.SpellOffset, (m, v) => m.SpellOffset = v);
			OnPropertyChanged(nameof(SelectedMove));
		}
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
