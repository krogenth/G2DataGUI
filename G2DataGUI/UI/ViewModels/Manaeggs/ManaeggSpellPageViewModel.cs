using System.Collections.Generic;
using G2DataGUI.UI.ViewModels.Manaeggs;

namespace G2DataGUI.UI.ViewModels.Manaeggs;

/// <summary>
/// One in-game "page" of a Manaegg's spell list — the actual game UI shows
/// the 18 spells as 3 pages of 6, not one flat list, so the editor matches
/// that instead of inventing its own organization. Spells here is a
/// grouping over the SAME ManaeggSpellViewModel instances
/// ManaeggViewModel.Spells already holds, not a separate copy — editing a
/// spell through a page's list is editing the exact same object the flat
/// Spells list (still exposed on ManaeggViewModel, e.g. for CSV export)
/// would show.
///
/// Plain data holder, not a BaseViewModel — PageNumber/Spells are fixed at
/// construction and never change afterward, so there's nothing for
/// INotifyPropertyChanged to ever announce.
/// </summary>
public sealed class ManaeggSpellPageViewModel(int pageNumber, IReadOnlyList<ManaeggSpellViewModel> spells)
{
	public int PageNumber { get; } = pageNumber;
	public string Title => $"Page {PageNumber}";
	public IReadOnlyList<ManaeggSpellViewModel> Spells { get; } = spells;
}
