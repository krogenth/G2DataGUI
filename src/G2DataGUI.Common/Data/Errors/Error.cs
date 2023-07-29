using Avalonia.Media;

namespace G2DataGUI.Common.Data.Errors;

public class Error
{
	public string Area { get; }
	public string Message { get; }
	public Color ErrorColor { get; }

	private Error() { }

	public Error(string area, string message)
	{
		Area = area;
		Message = message;
	}

	public void Remove() =>
		Errors.Instance.RemoveError(this);
}
