using Avalonia.Controls;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views.Content;

public partial class Enemies : UserControl
{
    public Enemies()
    {
        DataContext = EnemiesViewModel.Instance;
        InitializeComponent();
		Load();
    }

	private void Load()
	{
		EnemyPages.Children.Clear();
		EnemyNavPanel.SelectionChanged += EnemyNavPanelOnSelectionChange;
		EnemyNavPanel.SelectedItem = EnemyNavPanel.MenuItems.ElementAt(0);
	}

	private void EnemyNavPanelOnSelectionChange(object sender, NavigationViewSelectionChangedEventArgs e)
	{
		if (e.SelectedItem is NavigationViewItem navigationViewItem && navigationViewItem.Tag is not null)
		{
			switch (navigationViewItem.Tag.ToString())
			{
				case "EnemyStatsPage":
					EnemyNavPanel.Content = EnemyStatsPage;
					break;
				case "EnemyAISectionPage":
					EnemyNavPanel.Content = EnemyAISectionPage;
					break;
				case "EnemyMovesetPage":
					EnemyNavPanel.Content = EnemyMovesetPage;
					break;
			}
		}
	}
}
