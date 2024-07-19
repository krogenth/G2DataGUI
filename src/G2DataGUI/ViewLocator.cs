using Avalonia.Controls;
using Avalonia.Controls.Templates;
using G2DataGUI.UI.Common.ViewModels;
using System;

namespace G2DataGUI;

public class ViewLocator : IDataTemplate
{
	public Control? Build(object? data)
	{
		if (data is null)
		{
			return null;
		}

	var name = data.GetType().FullName!.Replace("ViewModel", "View", StringComparison.Ordinal);
		var type = Type.GetType(name);

		if (type != null)
		{
			var control = (Control)Activator.CreateInstance(type)!;
			control.DataContext = data;
			return control;
		}

		return new TextBlock { Text = "Not Found: " + name };
	}

	public bool Match(object? data) => data is BaseViewModel;
}
