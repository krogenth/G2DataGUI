using System;
using System.ComponentModel;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using G2DataGUI.Domain.Chr;
using G2DataGUI.Domain.Models;
using G2DataGUI.UI.ViewModels.Maps;

namespace G2DataGUI.UI.Views.Maps;

/// <summary>
/// The one page in this app whose code-behind does more than
/// InitializeComponent() — ModelViewport (G2DataGUI.Rendering) is a plain
/// Control with imperative SetModel/SetAnimation methods, not something a
/// XAML binding can drive directly, so selecting a map/model/motion all
/// get bridged here rather than through pure bindings.
///
/// Model/Motion pickers use one-way SelectedItem bindings (reflecting
/// MapViewModel.SelectedModel/SelectedMotion, both private-set) plus
/// SelectionChanged handlers that call MapViewModel.SelectModel/
/// SelectMotion explicitly — same shape as MainWindow's Normal/Hard
/// RadioButtons, for the same reason: these trigger real parsing work
/// (NjModelReader/NjMotionReader), not just a property flip, so an
/// explicit handler is clearer than a two-way binding with side effects
/// hidden in a property setter.
///
/// Playback: _animationTimer advances a frame counter at a fixed rate
/// (30fps — a reasonable assumption for this era of console game data,
/// not a confirmed value; if animations look too fast/slow, this is the
/// first thing to tune) and pushes it into Viewport.SetAnimation every
/// tick. Starts when a motion is selected, stops when cleared back to
/// bind pose (SelectMotion(null)) or when the map/model changes out from
/// under it.
/// </summary>
public partial class MapsView : UserControl
{
	private const double FramesPerSecond = 30.0;

	private DispatcherTimer? _animationTimer;
	private float _animationFrame;

	public MapsView()
	{
		InitializeComponent();

		DataContextChanged += OnDataContextChanged;
		WireframeToggle.IsCheckedChanged += (_, _) => Viewport.Wireframe = WireframeToggle.IsChecked == true;
		PointsToggle.IsCheckedChanged += (_, _) => Viewport.Points = PointsToggle.IsChecked == true;
	}

	private void OnDataContextChanged(object? sender, EventArgs e)
	{
		if (DataContext is MapsViewModel viewModel)
		{
			viewModel.PropertyChanged += OnViewModelPropertyChanged;
		}
	}

	private async void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
	{
		if (e.PropertyName != nameof(MapsViewModel.SelectedItem) || sender is not MapsViewModel viewModel)
		{
			return;
		}

		StopAnimation();

		MapViewModel? selected = viewModel.SelectedItem;

		if (selected is null || !selected.HasChrArchive)
		{
			Viewport.SetModel(null);
			return;
		}

		await selected.LoadPreviewAsync().ConfigureAwait(true);

		Viewport.SetModel(selected.PreviewModel, selected.PreviewTextures);
	}

	private void OnModelSelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		if (DataContext is not MapsViewModel { SelectedItem: { } map } || e.AddedItems.Count == 0)
		{
			return;
		}

		if (e.AddedItems[0] is not ChrModel chrModel)
		{
			return;
		}

		StopAnimation();

		map.SelectModel(chrModel);

		Viewport.SetModel(map.PreviewModel, map.PreviewTextures);
	}

	private void OnMotionSelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		if (DataContext is not MapsViewModel { SelectedItem: { } map })
		{
			return;
		}

		// A cleared/placeholder selection reports AddedItems.Count == 0 —
		// that's the "back to bind pose" case, so pass null through
		// rather than returning early the way OnModelSelectionChanged
		// does (there's no equivalent "no model" state to fall back to).
		MotionAnimation? motion = e.AddedItems.Count > 0 ? e.AddedItems[0] as MotionAnimation : null;

		map.SelectMotion(motion);

		StartOrStopAnimation(map.PreviewMotion);
	}

	private void OnResetCameraClick(object? sender, RoutedEventArgs e) => Viewport.ResetCamera();

	private void StartOrStopAnimation(NjMotion? motion)
	{
		StopAnimation();

		if (motion is null)
		{
			Viewport.SetAnimation(null, 0f);
			return;
		}

		int frameCount = Math.Max(motion.FrameCount, 1);
		DateTime lastTick = DateTime.UtcNow;

		_animationTimer = new DispatcherTimer
		{
			Interval = TimeSpan.FromSeconds(1.0 / 60.0)
		};

		_animationTimer.Tick += (_, _) =>
		{
			DateTime now = DateTime.UtcNow;
			float elapsedSeconds = (float)(now - lastTick).TotalSeconds;
			lastTick = now;

			_animationFrame = (_animationFrame + (elapsedSeconds * (float)FramesPerSecond)) % frameCount;

			Viewport.SetAnimation(motion, _animationFrame);
		};

		_animationTimer.Start();
	}

	private void StopAnimation()
	{
		_animationTimer?.Stop();
		_animationTimer = null;
		_animationFrame = 0f;
	}
}
