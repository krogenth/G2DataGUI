using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using G2DataGUI.UI.ViewModels;
using G2DataGUI.UI.Windows;

namespace G2DataGUI;

public partial class App : Application
{
	public override void Initialize()
	{
		AvaloniaXamlLoader.Load(this);
	}

	public override void OnFrameworkInitializationCompleted()
	{
		if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
		{
			desktop.MainWindow = new MainWindow
			{
				DataContext = new MainWindowViewModel(desktop.MainWindow),
			};
		}

		base.OnFrameworkInitializationCompleted();
	}
}
