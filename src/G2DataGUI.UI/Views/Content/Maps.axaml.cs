using Avalonia.Controls;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public sealed partial class Maps : UserControl
{
	public Maps()
	{
		InitializeComponent();
		Load();
	}

	private void Load()
	{
		MapPages.Children.Clear();
		MapNavPanel.SelectionChanged += MapNavPanelOnSelectionChange;
		MapNavPanel.SelectedItem = MapNavPanel.MenuItems.ElementAt(0);
	}

	private void MapNavPanelOnSelectionChange(object? sender, NavigationViewSelectionChangedEventArgs e)
	{
		if (e.SelectedItem is NavigationViewItem navigationViewItem && navigationViewItem.Tag is not null)
		{
			switch (navigationViewItem.Tag.ToString())
			{
				case "MapEntriesPage":
					//MapEntriesPage.DataContext = new MapEntriesViewModel((DataContext as MapsViewModel)!.SelectedMapItem.Entries);
					MapNavPanel.Content = MapEntriesPage;
					break;
				case "MapInstancesPage":
                    //MapInstancesPage.DataContext = new MapInstancesViewModel((DataContext as MapsViewModel)!.SelectedMapItem.Instances);
                    MapNavPanel.Content = MapInstancesPage;
					break;
				case "MapHTAsPage":
                    //MapEntriesPage.DataContext = new MapEntriesViewModel((DataContext as MapsViewModel)!.SelectedMapItem.Entries);
                    MapNavPanel.Content = MapHTAsPage;
					break;
				case "MapScriptsPage":
                    //MapEntriesPage.DataContext = new MapEntriesViewModel((DataContext as MapsViewModel)!.SelectedMapItem.Entries);
                    MapNavPanel.Content = MapScriptsPage;
					break;
				case "MapEnemyPositionsPage":
                    //MapEntriesPage.DataContext = new MapEntriesViewModel((DataContext as MapsViewModel)!.SelectedMapItem.Entries);
                    MapNavPanel.Content = MapEnemyPositionsPage;
					break;
				case "MapEnemyGroupsPage":
                    //MapEntriesPage.DataContext = new MapEntriesViewModel((DataContext as MapsViewModel)!.SelectedMapItem.Entries);
                    MapNavPanel.Content = MapEnemyGroupsPage;
					break;
				case "MapMOSsPage":
                    //MapEntriesPage.DataContext = new MapEntriesViewModel((DataContext as MapsViewModel)!.SelectedMapItem.Entries);
                    MapNavPanel.Content = MapMOSsPage;
					break;
				case "MapIconsPage":
                    //MapEntriesPage.DataContext = new MapEntriesViewModel((DataContext as MapsViewModel)!.SelectedMapItem.Entries);
                    MapNavPanel.Content = MapIconsPage;
					break;
				case "MapShopPage":
                    //MapEntriesPage.DataContext = new MapEntriesViewModel((DataContext as MapsViewModel)!.SelectedMapItem.Entries);
                    MapNavPanel.Content = MapShopPage;
					break;
				case "MapDialoguePage":
                    //MapEntriesPage.DataContext = (DataContext as MapsViewModel)!.SelectedMapEntriesViewModel;
                    MapNavPanel.Content = MapDialoguePage;
					break;
			}
		}
	}
}
