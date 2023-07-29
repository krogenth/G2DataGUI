using Avalonia.Controls;
using Avalonia.Interactivity;
using G2DataGUI.Common.Data.Errors;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Views;

public partial class Errors : UserControl
{
    public Errors()
    {
		DataContext = ErrorsViewModel.Instance;
        InitializeComponent();
    }

	private void OnErrorCloseClick(object sender, RoutedEventArgs e)
	{
		if (sender is Button button)
		{
			if (button.DataContext is Error error)
			{
				error.Remove();
			}
		}
	}
}
