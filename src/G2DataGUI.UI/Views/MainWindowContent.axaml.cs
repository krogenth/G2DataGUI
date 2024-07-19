using Avalonia.Controls;
using FluentAvalonia.UI.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views;

public partial class MainWindowContent : UserControl
{
	public MainWindowContent()
	{
		DataContext = new MainWindowContentViewModel();
		InitializeComponent();
        Load();
	}

    private void Load()
    {
        Pages.Children.Clear();
        NavPanel.SelectionChanged += NavPanelOnSelectionChanged;
        NavPanel.SelectedItem = NavPanel.MenuItems.ElementAt(0);
    }

    private void NavPanelOnSelectionChanged(object? sender, NavigationViewSelectionChangedEventArgs e)
    {
        if (e.SelectedItem is NavigationViewItem navigationViewItem && navigationViewItem.Tag is not null)
        {
            switch (navigationViewItem.Tag.ToString())
            {
                case "MovesPage":
					MovesPage.DataContext = (DataContext as MainWindowContentViewModel)!.MovesViewModel;
                    NavPanel.Content = MovesPage;
                    break;
                case "ManaeggsPage":
					ManaeggsPage.DataContext = (DataContext as MainWindowContentViewModel)!.ManaeggsViewModel;
					NavPanel.Content = ManaeggsPage;
                    break;
                case "SkillsPage":
					SkillsPage.DataContext = (DataContext as MainWindowContentViewModel)!.SkillsViewModel;
					NavPanel.Content = SkillsPage;
                    break;
                case "SkillbooksPage":
					SkillbooksPage.DataContext = (DataContext as MainWindowContentViewModel)!.SkillbooksViewModel;
					NavPanel.Content = SkillbooksPage;
                    break;
                case "SpecialsPage":
					SpecialsPage.DataContext = (DataContext as MainWindowContentViewModel)!.SpecialsViewModel;
					NavPanel.Content = SpecialsPage;
                    break;
                case "ItemsPage":
					ItemsPage.DataContext = (DataContext as MainWindowContentViewModel)!.ItemsViewModel;
					NavPanel.Content = ItemsPage;
                    break;
                case "StartingStatsPage":
					StartingStatsPage.DataContext = (DataContext as MainWindowContentViewModel)!.StartingStatsViewModel;
					NavPanel.Content = StartingStatsPage;
                    break;
                case "LevelupsPage":
					LevelupsPage.DataContext = (DataContext as MainWindowContentViewModel)!.LevelupsViewModel;
					NavPanel.Content = LevelupsPage;
                    break;
                case "EnemiesPage":
					EnemiesPage.DataContext = (DataContext as MainWindowContentViewModel)!.EnemiesViewModel;
					NavPanel.Content = EnemiesPage;
                    break;
                case "BossesPage":
					BossesPage.DataContext = (DataContext as MainWindowContentViewModel)!.BossesViewModel;
					NavPanel.Content = BossesPage;
                    break;
                case "MapsPage":
					MapsPage.DataContext = (DataContext as MainWindowContentViewModel)!.MapsViewModel;
					NavPanel.Content = MapsPage;
                    break;
            }
        }
    }
}
