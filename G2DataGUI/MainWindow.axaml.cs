using System.Collections.Generic;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using G2DataGUI.Common;
using G2DataGUI.Contexts;
using G2DataGUI.ViewModels;

namespace G2DataGUI;

public partial class MainWindow : Window
{
	public MainWindow()
	{
		InitializeComponent();

		// No project loaded yet — DataContext starts with an empty
		// navigation list until the user picks a folder via File > Open
		// Project (OnOpenProjectClick below). This is the composition
		// root: it's the one place in the app that's allowed to know how
		// to build a GameProjectContext/GameProjectViewModel from a raw
		// path — everything downstream (MainViewModel, every page
		// ViewModel) just receives the already-built result.
		DataContext = new MainViewModel();
	}

	private async void OnOpenProjectClick(object? sender, RoutedEventArgs e)
	{
		IReadOnlyList<IStorageFolder> folders = await StorageProvider.OpenFolderPickerAsync(
			new FolderPickerOpenOptions
			{
				Title = "Select the game data root",
				AllowMultiple = false
			}).ConfigureAwait(true);

		string? path = folders.Count > 0 ? folders[0].TryGetLocalPath() : null;

		if (path is null)
		{
			return;
		}

		GameProjectPaths paths = new(path);
		GameProjectContext context = new(paths, GameVersion.Normal);

		// Reuse the same static instance the XAML LocaleExtension binds
		// to, so ViewModel-resolved names and bound XAML text stay in
		// sync on a language change — don't construct a second,
		// independent LocaleService.
		GameProjectViewModel project = new(context, UI.Common.Locale.LocaleService.Instance);

		if (DataContext is MainViewModel mainViewModel)
		{
			await mainViewModel.LoadProjectAsync(project).ConfigureAwait(true);
		}
	}

	private async void OnSaveClick(object? sender, RoutedEventArgs e)
	{
		if (DataContext is MainViewModel mainViewModel)
		{
			await mainViewModel.SaveAsync().ConfigureAwait(true);
		}
	}

	private async void OnNormalVersionClick(object? sender, RoutedEventArgs e)
	{
		if (DataContext is MainViewModel mainViewModel)
		{
			await mainViewModel.SwitchVersionAsync(GameVersion.Normal).ConfigureAwait(true);
		}
	}

	private async void OnHardVersionClick(object? sender, RoutedEventArgs e)
	{
		if (DataContext is MainViewModel mainViewModel)
		{
			await mainViewModel.SwitchVersionAsync(GameVersion.Hard).ConfigureAwait(true);
		}
	}
}
