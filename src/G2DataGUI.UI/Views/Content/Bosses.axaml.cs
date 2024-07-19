using Avalonia.Controls;
using FluentAvalonia.Core;
using FluentAvalonia.UI.Controls;

namespace G2DataGUI.UI.Views.Content;

public partial class Bosses : UserControl
{
	public Bosses()
	{
		InitializeComponent();
		Load();
	}

	private void Load()
	{
		BossPages.Children.Clear();
		BossNavPanel.SelectionChanged += BossNavPanelOnSelectionChange;
		BossNavPanel.SelectedItem = BossNavPanel.MenuItems.ElementAt(0);
	}

	private void BossNavPanelOnSelectionChange(object? sender, NavigationViewSelectionChangedEventArgs e)
	{
		if (e.SelectedItem is NavigationViewItem navigationViewItem && navigationViewItem.Tag is not null)
		{
			switch (navigationViewItem.Tag.ToString())
			{
				case "BossStatsPage":
					BossNavPanel.Content = BossStatsPage;
					break;
				case "BossAISectionPage":
					BossNavPanel.Content = BossAISectionPage;
					break;
				case "BossMovesetsPage":
					BossNavPanel.Content = BossMovesetsPage;
					break;
			}
		}
	}
}
