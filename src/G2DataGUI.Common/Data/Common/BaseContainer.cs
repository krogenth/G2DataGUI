using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace G2DataGUI.Common.Data.Common;

public class BaseContainer : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler? PropertyChanged;

	protected void NotifyPropertyChanged([CallerMemberName] string propertyName = "") =>
		PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}
