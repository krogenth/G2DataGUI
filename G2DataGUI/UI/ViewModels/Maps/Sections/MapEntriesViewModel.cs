using System.Collections.Generic;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Maps.Sections;

/// <summary>Non-generic subclass of MapSectionViewModel<MapEntryViewModel>
/// purely so XAML's x:DataType can name it directly — Avalonia does support
/// generic type arguments in x:DataType (the vm:MapSectionViewModel(T)
/// syntax), but with 8 of these sections needing it, a concrete name per
/// section is the safer bet over relying on that syntax compiling
/// correctly 8 times.</summary>
public sealed class MapEntriesViewModel(IEnumerable<MapEntryViewModel> items) : MapSectionViewModel<MapEntryViewModel>(items);
