using Avalonia.Controls;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public sealed partial class Maps : UserControl
{
    public Maps()
    {
        DataContext = MapsViewModel.Instance;
        InitializeComponent();
		Load();
    }

	private void Load()
	{
		MapPages.Children.Clear();
		MapNavPanel.SelectionChanged += MapNavPanelOnSelectionChange;
		MapNavPanel.SelectedItem = MapNavPanel.MenuItems.ElementAt(0);
	}

	private void MapNavPanelOnSelectionChange(object sender, NavigationViewSelectionChangedEventArgs e)
	{
		if (e.SelectedItem is NavigationViewItem navigationViewItem && navigationViewItem.Tag is not null)
		{
			switch (navigationViewItem.Tag.ToString())
			{
				case "MapEntriesPage":
					MapNavPanel.Content = MapEntriesPage;
					break;
				case "MapInstancesPage":
					MapNavPanel.Content = MapInstancesPage;
					break;
				case "MapHTAsPage":
					MapNavPanel.Content = MapHTAsPage;
					break;
				case "MapScriptsPage":
					MapNavPanel.Content = MapScriptsPage;
					break;
				case "MapEnemyPositionsPage":
					MapNavPanel.Content = MapEnemyPositionsPage;
					break;
				case "MapEnemyGroupsPage":
					MapNavPanel.Content = MapEnemyGroupsPage;
					break;
				case "MapMOSsPage":
					MapNavPanel.Content = MapMOSsPage;
					break;
				case "MapIconsPage":
					MapNavPanel.Content = MapIconsPage;
					break;
				case "MapShopPage":
					MapNavPanel.Content = MapShopPage;
					break;
			}
		}
	}
}
