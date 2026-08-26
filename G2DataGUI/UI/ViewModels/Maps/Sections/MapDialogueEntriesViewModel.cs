using System.Collections.Generic;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

public sealed class MapDialogueEntriesViewModel(IEnumerable<MapDialogueEntryViewModel> items)
	: MapSectionViewModel<MapDialogueEntryViewModel>(items);
