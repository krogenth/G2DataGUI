using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

/// <summary>
/// One entry in the left-hand navigation list — a label plus the
/// page ViewModel to show when selected. ViewModel is created eagerly
/// (not lazily on first selection) since every feature's data is already
/// loaded as part of GameProjectContext by the time MainViewModel builds
/// the navigation list — there's no expensive work to defer.
/// </summary>
public sealed class NavigationItem(string label, BaseViewModel viewModel)
{
	public string Label { get; } = label;
	public BaseViewModel ViewModel { get; } = viewModel;
}
