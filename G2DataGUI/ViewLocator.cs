using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI;

/// <summary>
/// Convention-based ViewModel-to-View resolution — the standard
/// lightweight Avalonia MVVM navigation pattern (no routing framework,
/// no third-party control library). "Foo.Bar.MovesViewModel" resolves to
/// "Foo.Bar.MovesView" in the same namespace; the View is expected to be
/// a Control with a parameterless constructor that sets its own
/// DataContext via binding (Avalonia's normal XAML DataContext
/// inheritance already does this once this template hands back an
/// instance — no manual wiring needed here).
///
/// Registered once, application-wide, in App.axaml's
/// Application.DataTemplates — every ContentControl/ItemsControl in the
/// app automatically uses it for any ViewModel it doesn't have a more
/// specific DataTemplate for.
/// </summary>
public sealed class ViewLocator : IDataTemplate
{
	public Control Build(object? data)
	{
		if (data is null)
		{
			return new TextBlock { Text = "(no view model)" };
		}

		string viewModelName = data.GetType().FullName!;
		string viewName = viewModelName.Replace("ViewModel", "View");
		Type? viewType = Type.GetType(viewName);

		if (viewType is null)
		{
			return new TextBlock
			{
				Text = $"No view registered for {viewModelName} (expected {viewName})"
			};
		}

		return (Control)Activator.CreateInstance(viewType)!;
	}

	public bool Match(object? data) => data is BaseViewModel;
}
