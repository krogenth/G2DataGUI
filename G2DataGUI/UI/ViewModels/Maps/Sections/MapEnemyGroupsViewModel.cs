using System.Collections.Generic;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

public sealed class MapEnemyGroupsViewModel(IEnumerable<MapEnemyGroupViewModel> items) : MapSectionViewModel<MapEnemyGroupViewModel>(items);
