using Avalonia;
using Avalonia.Controls;
using G2DataGUI.UI.ViewModels;

namespace G2DataGUI.UI.Windows;

public partial class AudioViewerWindow : Window
{
    public AudioViewerWindow()
    {
		DataContext = AudioViewerWindowViewModel.Instance;
		InitializeComponent();
#if DEBUG
		this.AttachDevTools();
#endif
	}
}
