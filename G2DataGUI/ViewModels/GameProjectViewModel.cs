using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using G2DataGUI.Common;
using G2DataGUI.Contexts;
using G2DataGUI.Locale;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels;

/// <summary>
/// Root ViewModel for a loaded project. The piece that matters for version
/// switching: Current is a GameVersionViewModel, not fixed at construction —
/// SwitchVersionAsync replaces it (raising PropertyChanged) after telling
/// the underlying GameProjectContext to switch, which loads that version's
/// Enemies/Bosses from disk the first time (GameProjectContext.
/// SwitchVersionAsync only hits disk once per version; later switches back
/// to an already-visited version are instant). Anything bound to
/// Current.Enemies/Current.Bosses — a tab, a list — updates the moment
/// Current itself changes, without needing to know a switch happened.
/// GameVersionViewModel instances are cached per GameVersion here, mirroring
/// GameProjectContext's own _versions cache, so switching back to a visited
/// version doesn't rebuild its ViewModels either.
/// </summary>
public sealed class GameProjectViewModel : BaseViewModel
{
	private readonly GameProjectContext _project;
	private readonly Dictionary<GameVersion, GameVersionViewModel> _versionViewModels = [];

	public GameProjectViewModel(GameProjectContext project, ILocalizationService locale)
	{
		_project = project;

		Global = new GlobalDataViewModel(project.Global, locale);
	}

	public GlobalDataViewModel Global { get; }

	public GameVersion CurrentVersion => _project.CurrentVersion;

	/// <summary>Not valid until LoadAsync has completed — same contract as
	/// GameProjectContext.Current, which this wraps.</summary>
	public GameVersionViewModel Current { get; private set; } = null!;

	public async Task LoadAsync(CancellationToken cancellationToken = default)
	{
		await _project.LoadAsync(cancellationToken).ConfigureAwait(false);

		SetCurrent(_project.Current);
	}

	public async Task SwitchVersionAsync(GameVersion version, CancellationToken cancellationToken = default)
	{
		if (version == CurrentVersion)
		{
			return;
		}

		await _project.SwitchVersionAsync(version, cancellationToken).ConfigureAwait(false);

		SetCurrent(_project.Current);
	}

	private void SetCurrent(GameVersionContext context)
	{
		if (!_versionViewModels.TryGetValue(context.Version, out GameVersionViewModel? viewModel))
		{
			viewModel = new GameVersionViewModel(context);
			_versionViewModels[context.Version] = viewModel;
		}

		Current = viewModel;

		OnPropertyChanged(nameof(Current));
		OnPropertyChanged(nameof(CurrentVersion));
	}
}
