using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using G2DataGUI.Common;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Bosses;
using G2DataGUI.UI.ViewModels.Enemies;
using G2DataGUI.UI.ViewModels.Items;
using G2DataGUI.UI.ViewModels.Levelups;
using G2DataGUI.UI.ViewModels.Manaeggs;
using G2DataGUI.UI.ViewModels.Moves;
using G2DataGUI.UI.ViewModels.Skillbooks;
using G2DataGUI.UI.ViewModels.Skills;
using G2DataGUI.UI.ViewModels.Specials;
using G2DataGUI.UI.ViewModels.Starting;

namespace G2DataGUI.UI.ViewModels;

/// <summary>
/// The app shell: a navigation list on one side, CurrentPage's View (found
/// by ViewLocator) on the other. Rebuilds the navigation list whenever
/// Project changes (a project is opened) or Project.CurrentVersion changes
/// (Enemies/Bosses need to point at whichever GameVersionViewModel is
/// currently active — see GameProjectViewModel's own doc comment on why
/// Current isn't fixed).
///
/// Not wired to an "Open Project" flow yet — see LoadProjectAsync's doc
/// comment.
/// </summary>
public sealed class MainViewModel : BaseViewModel
{
	private GameProjectViewModel? _project;

	public GameProjectViewModel? Project
	{
		get => _project;
		private set => SetProject(value);
	}

	public List<NavigationItem> NavigationItems { get; private set; } = [];

	public NavigationItem? SelectedItem
	{
		get;
		set
		{
			if (ReferenceEquals(field, value))
			{
				return;
			}

			field = value;
			OnPropertyChanged(nameof(SelectedItem));
			OnPropertyChanged(nameof(CurrentPage));
		}
	}

	public BaseViewModel? CurrentPage => SelectedItem?.ViewModel;

	public bool IsNormalVersion => _project?.CurrentVersion == GameVersion.Normal;
	public bool IsHardVersion => _project?.CurrentVersion == GameVersion.Hard;

	/// <summary>
	/// True while a Save/Open/version-switch is in flight — bind
	/// a control's IsEnabled to "!IsBusy" so a second click can't overlap
	/// an operation already running (SaveAsync/SwitchVersionAsync below
	/// aren't reentrant-safe, and don't need to be as long as the UI
	/// enforces this instead).
	/// </summary>
	public bool IsBusy
	{
		get;
		private set
		{
			if (field == value)
			{
				return;
			}

			field = value;
			OnPropertyChanged(nameof(IsBusy));
		}
	}

	/// <summary>
	/// Set by SaveAsync/SwitchVersionAsync/LoadProjectAsync when
	/// the underlying operation throws — null the rest of the time. Bind
	/// a TextBlock (Classes="error", already themed) to this so a failed
	/// save or a failed version switch doesn't just silently do nothing.
	/// </summary>
	public string? ErrorMessage
	{
		get;
		private set
		{
			if (field == value)
			{
				return;
			}

			field = value;
			OnPropertyChanged(nameof(ErrorMessage));
		}
	}

	/// <summary>
	/// Called by MainWindow.axaml.cs's OnOpenProjectClick once the
	/// user has picked a folder and it's been turned into a
	/// GameProjectViewModel — see this class's own doc comment for why
	/// that construction happens there, not here.
	/// </summary>
	public async Task LoadProjectAsync(GameProjectViewModel project, CancellationToken cancellationToken = default)
	{
		IsBusy = true;
		ErrorMessage = null;

		try
		{
			await project.LoadAsync(cancellationToken).ConfigureAwait(true);

			Project = project;
		}
		catch (Exception ex)
		{
			ErrorMessage = $"Couldn't open project: {ex.Message}";
		}
		finally
		{
			IsBusy = false;
			OnPropertyChanged(nameof(IsNormalVersion));
			OnPropertyChanged(nameof(IsHardVersion));
		}
	}

	/// <summary>
	/// Saves everything dirty across the whole loaded project —
	/// see GameProjectViewModel.SaveAsync's own doc comment. Safe to call
	/// even when nothing's changed (a no-op at the Context/Repository
	/// layer, not skipped here) and safe to call with no project loaded
	/// (just does nothing) — a Save button can call this unconditionally
	/// rather than needing its own "is there anything to save" check.
	/// </summary>
	public async Task SaveAsync(CancellationToken cancellationToken = default)
	{
		if (_project is null)
		{
			return;
		}

		IsBusy = true;
		ErrorMessage = null;

		try
		{
			await _project.SaveAsync(cancellationToken).ConfigureAwait(true);
		}
		catch (Exception ex)
		{
			ErrorMessage = $"Couldn't save: {ex.Message}";
		}
		finally
		{
			IsBusy = false;
		}
	}

	/// <summary>
	/// Switches Normal/Hard. Deliberately a method the View calls
	/// from an event handler (a RadioButton/ToggleSwitch's Click/
	/// IsCheckedChanged in code-behind) rather than a two-way-bound bool
	/// property with async work hidden in its setter — same reasoning as
	/// Save being a button+handler rather than an auto-triggered command:
	/// explicit error handling (ErrorMessage below) and an obvious point
	/// where IsBusy gets set, instead of a property setter silently
	/// kicking off unawaited work.
	/// </summary>
	public async Task SwitchVersionAsync(GameVersion version, CancellationToken cancellationToken = default)
	{
		if (_project is null)
		{
			return;
		}

		IsBusy = true;
		ErrorMessage = null;

		try
		{
			await _project.SwitchVersionAsync(version, cancellationToken).ConfigureAwait(true);
		}
		catch (Exception ex)
		{
			ErrorMessage = $"Couldn't switch version: {ex.Message}";
		}
		finally
		{
			IsBusy = false;
		}
	}

	private void SetProject(GameProjectViewModel? project)
	{
		_project?.PropertyChanged -= OnProjectPropertyChanged;

		_project = project;

		_project?.PropertyChanged += OnProjectPropertyChanged;

		RebuildNavigation();

		OnPropertyChanged(nameof(Project));
	}

	private void OnProjectPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
	{
		// Enemies/Bosses navigation items need to point at whichever
		// GameVersionViewModel is current whenever the user switches
		// Normal/Hard — rebuilding the whole list is simpler and cheap
		// (a dozen list entries) compared to patching just those two.
		if (e.PropertyName == nameof(GameProjectViewModel.Current))
		{
			RebuildNavigation();
		}
	}

	private void RebuildNavigation()
	{
		List<NavigationItem> items = [];

		if (_project is not null)
		{
			GlobalDataViewModel global = _project.Global;

			items.Add(new NavigationItem("Moves", global.Moves));
			items.Add(new NavigationItem("Manaeggs", global.Manaeggs));
			items.Add(new NavigationItem("Skills", global.Skills));
			items.Add(new NavigationItem("Skillbooks", global.Skillbooks));
			items.Add(new NavigationItem("Levelups", global.Levelups));
			items.Add(new NavigationItem("Items", global.Items));
			items.Add(new NavigationItem("Specials", global.Specials));
			items.Add(new NavigationItem("Initial", global.Initial));
			items.Add(new NavigationItem("Maps", global.Maps));

			if (_project.Current is not null)
			{
				items.Add(new NavigationItem("Enemies", _project.Current.Enemies));
				items.Add(new NavigationItem("Bosses", _project.Current.Bosses));
			}
		}

		NavigationItems = items;
		OnPropertyChanged(nameof(NavigationItems));

		SelectedItem = items.FirstOrDefault();
	}
}
