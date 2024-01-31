using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using G2DataGUI.Events;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class AudioViewerWindowViewModel : BaseViewModel
{
	public static AudioViewerWindowViewModel Instance { get; private set; } = new();

	private AudioViewerWindowViewModel()
	{

	}

	public void OnAudioChange(object sender, AudioEventArgs e) { }
}
