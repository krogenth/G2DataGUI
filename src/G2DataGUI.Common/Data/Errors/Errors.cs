using System.Collections.ObjectModel;
using G2DataGUI.Common.Data.Common;

namespace G2DataGUI.Common.Data.Errors;

public class Errors : BaseContainer
{
	public static Errors Instance = new();

	public ObservableCollection<Error> AppErrors = new();

	private Errors() { }

	public void AddError(Error error) => AppErrors.Add(error);

	public void AddError(string message, string area) =>
		AppErrors.Add(new Error(message, area));

	public void RemoveError(Error error) => AppErrors.Remove(error);
}
