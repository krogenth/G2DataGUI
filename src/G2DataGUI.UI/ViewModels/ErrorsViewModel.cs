using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Errors;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class ErrorsViewModel : BaseViewModel
{
	public ObservableCollection<Error> AppErrors { get; set; } = Errors.Instance.AppErrors;

	public static ErrorsViewModel Instance { get; set; } = new();

	private ErrorsViewModel() { }
}
