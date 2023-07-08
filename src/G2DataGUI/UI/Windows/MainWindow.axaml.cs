using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using FluentAvalonia.Styling;
using G2DataGUI.Events;
using G2DataGUI.UI.ViewModels;
using System;

namespace G2DataGUI.UI.Windows;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
#if DEBUG
        this.AttachDevTools();
#endif
        AttachEventCallbacks();
    }

    private void AttachEventCallbacks()
    {
        MenuBarViewModel.Instance.Window = this;
        MenuBarViewModel.Instance.StyleThemeChanged += OnStyleThemeChange;
        UpdateStyleTheme(MenuBarViewModel.Instance.SelectedStyle);
    }

	public void OnStyleThemeChange(object sender, StyleThemeEventArgs e) => UpdateStyleTheme(e.Style);

	private void UpdateStyleTheme(string style)
    {
        var theme = AvaloniaLocator.Current.GetService<FluentAvaloniaTheme>();
        theme.RequestedTheme = style;

        string baseStyleUri = "avares://G2DataGUI/Assets/Styles/Base{0}.xaml";
        Uri stylesUri = new(string.Format(baseStyleUri, style), UriKind.Absolute);
        IStyle styles = AvaloniaXamlLoader.Load(stylesUri) as Styles;
        Styles.Clear();
        Styles.Add(styles);
    }
}