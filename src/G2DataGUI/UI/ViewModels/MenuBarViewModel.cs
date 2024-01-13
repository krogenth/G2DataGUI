using System;
using System.Diagnostics;
using System.IO;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.Common.Data.Moves;
using G2DataGUI.Common.Data.Skills;
using G2DataGUI.Events;
using G2DataGUI.UI.Windows;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.Common;
using G2DataGUI.Common.Data.Enemies;
using G2DataGUI.Common.Data.Bosses;
using G2DataGUI.Common.Data.Maps;
using G2DataGUI.Common.Data.Starting;
using G2DataGUI.Common.Data.Levelups;
using G2DataGUI.Common.Paths;

namespace G2DataGUI.UI.ViewModels;

public sealed class MenuBarViewModel : BaseViewModel
{
    public static MenuBarViewModel Instance { get; private set; } = new();
    public MainWindow Window { get; set; } = null;
    private string _selectedStyle = "Dark";
    public event EventHandler<StyleThemeEventArgs> StyleThemeChanged;

	private MenuBarViewModel() { }

    public string SelectedStyle
    {
        get => _selectedStyle;
        set
        {
            _selectedStyle = value;
            StyleThemeChanged?.Invoke(this, new StyleThemeEventArgs(_selectedStyle));
            OnPropertyChanged(nameof(SelectedStyle));
        }
    }

    public bool IsHardMode
    {
        get => DifficultyMode.Instance.IsHardMode;
        set => DifficultyMode.Instance.IsHardMode = value;
    }

    public void SaveAll()
    {
        Moves.Instance.Save();
        Skills.Instance.Save();
        Manaeggs.Instance.Save();
        Skillbooks.Instance.Save();
        SpecialSets.Instance.Save();
        Items.Instance.Save();
        StartingStats.Instance.Save();
		Levelups.Instance.Save();
		Enemies.Instance.Save();
		Bosses.Instance.Save();
		Maps.Instance.Save();
    }

    public void LaunchGame()
    {
        if (File.Exists("./Grandia2.exe"))
		{
			Process.Start("./Grandia2.exe");
		}
	}

    public void Reload()
    {
        Moves.Instance.Reload();
        Skills.Instance.Reload();
        Manaeggs.Instance.Reload();
        Skillbooks.Instance.Reload();
        SpecialSets.Instance.Reload();
        Items.Instance.Reload();
        StartingStats.Instance.Reload();
		Levelups.Instance.Reload();
		Enemies.Instance.Reload();
		Bosses.Instance.Reload();
    }

    public void GenerateCSVFiles()
    {
        if (!Directory.Exists(ProjectPaths.DocumentationDirectory))
        {
            Directory.CreateDirectory(ProjectPaths.DocumentationDirectory);
        }

        Moves.Instance.GenerateCSV();
        Skills.Instance.GenerateCSV();
        Manaeggs.Instance.GenerateCSV();
        Skillbooks.Instance.GenerateCSV();
        SpecialSets.Instance.GenerateCSV();
		StartingStats.Instance.GenerateCSV();
		Levelups.Instance.GenerateCSV();
		Enemies.Instance.GenerateCSV();
		Bosses.Instance.GenerateCSV();
    }

	public void Exit() => Window?.Close();

	public void OpenDDSViewer()
    {
        DDSViewerWindow ddsViewerWindow = new();
        ddsViewerWindow.Show();
    }
}
