using G2DataGUI.Common.Data.Items;
using G2DataGUI.Common.Data.Moves;
using G2DataGUI.Common.Data.Skills;
using G2DataGUI.Events;
using G2DataGUI.UI.Windows;
using System;
using System.Diagnostics;
using System.IO;

namespace G2DataGUI.UI.ViewModels;

public sealed class MenuBarViewModel : BaseViewModel
{
    public static MenuBarViewModel Instance { get; private set; } = new();
    public MainWindow Window { get; set; } = null;
    private string _selectedStyle = "Dark";
    public event EventHandler<StyleThemeEventArgs> StyleThemeChanged;

    public void SaveAll()
    {
        Moves.Instance.Save();
        Skills.Instance.Save();
        Manaeggs.Instance.Save();
        Skillbooks.Instance.Save();
        SpecialSets.Instance.Save();
        Items.Instance.Save();
    }

    public void LaunchGame()
    {
        if (File.Exists("./Grandia2.exe")) Process.Start("./Grandia2.exe");
    }

    public void Reload()
    {
        Moves.Instance.Reload();
        Skills.Instance.Reload();
        Manaeggs.Instance.Reload();
        Skillbooks.Instance.Reload();
        SpecialSets.Instance.Reload();
        Items.Instance.Reload();
    }

    public void Exit()
    {
        Window?.Close();
    }

    public void OpenDDSViewer()
    {
        DDSViewerWindow dDSViewerWindow = new DDSViewerWindow();
        dDSViewerWindow.Show();
    }

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
}