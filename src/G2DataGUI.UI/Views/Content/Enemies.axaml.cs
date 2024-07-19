using Avalonia.Controls;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class Enemies : UserControl
{
	public Enemies()
	{
		InitializeComponent();
		Load();
	}

	private void Load()
	{
		EnemyPages.Children.Clear();
		EnemyNavPanel.SelectionChanged += EnemyNavPanelOnSelectionChange;
		EnemyNavPanel.SelectedItem = EnemyNavPanel.MenuItems.ElementAt(0);
	}

	private void EnemyNavPanelOnSelectionChange(object? sender, NavigationViewSelectionChangedEventArgs e)
	{
		if (e.SelectedItem is NavigationViewItem navigationViewItem && navigationViewItem.Tag is not null)
		{
			switch (navigationViewItem.Tag.ToString())
			{
				case "EnemyStatsPage":
					//EnemyStatsPage.DataContext = new EnemyStatsViewModel((DataContext as EnemiesViewModel)!.SelectedEnemyItem.Stats);
					EnemyNavPanel.Content = EnemyStatsPage;
					break;
				case "EnemyAISectionPage":
                    //EnemyAISectionPage.DataContext = new EnemyAISectionViewModel((DataContext as EnemiesViewModel)!.SelectedEnemyItem.AISection);
                    EnemyNavPanel.Content = EnemyAISectionPage;
					break;
				case "EnemyMovesetPage":
                    //EnemyMovesetPage.DataContext = new EnemyMovesetViewModel((DataContext as EnemiesViewModel)!.SelectedEnemyItem.Moveset);
                    EnemyNavPanel.Content = EnemyMovesetPage;
					break;
			}
		}
	}
}
