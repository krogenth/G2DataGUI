using Avalonia.Controls;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public sealed class MainWindowViewModel(Window mainWindow) : BaseViewModel
{
    public MenuBarViewModel MenuBarViewModel { get; set; } = new(mainWindow);
}
