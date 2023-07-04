using Avalonia.Controls;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views;

public partial class WindowContent : UserControl
{
    public WindowContent()
    {
        DataContext = new WindowContentViewModel();
        InitializeComponent();
        Load();
    }

    private void Load()
    {
        Pages.Children.Clear();
        NavPanel.SelectionChanged += NavPanelOnSelectionChanged;
        NavPanel.SelectedItem = NavPanel.MenuItems.ElementAt(0);
    }

    private void NavPanelOnSelectionChanged(object sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (e.SelectedItem is NavigationViewItem navigationViewItem && navigationViewItem.Tag is not null)
        {
            switch (navigationViewItem.Tag.ToString())
            {
                case "MovesPage":
                    NavPanel.Content = MovesPage;
                    break;
                case "ManaeggsPage":
                    NavPanel.Content = ManaeggsPage;
                    break;
                case "SkillsPage":
                    NavPanel.Content = SkillsPage;
                    break;
                case "SkillbooksPage":
                    NavPanel.Content = SkillbooksPage;
                    break;
                case "SpecialsPage":
                    NavPanel.Content = SpecialsPage;
                    break;
                case "ItemsPage":
                    NavPanel.Content = ItemsPage;
                    break;
                case "EnemiesPage":
                    NavPanel.Content = EnemiesPage;
                    break;
            }
        }
    }
}