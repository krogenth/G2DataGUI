using Avalonia.Controls;
using G2DataGUI.Common;
using G2DataGUI.Contexts;

namespace G2DataGUI;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

		var globalPaths = new GamePaths("D:\\SteamLibrary\\steamapps\\common\\Grandia II Anniversary Edition\\content\\data\\afs");
		var versionPaths = new VersionGamePaths("D:\\SteamLibrary\\steamapps\\common\\Grandia II Anniversary Edition\\content\\data\\afs", GameVersion.Normal);
		var global = new GlobalDataContext(globalPaths);
		var version = new GameVersionContext(versionPaths);
    }
}
