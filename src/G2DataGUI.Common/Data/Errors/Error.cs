using Avalonia.Media;

namespace G2DataGUI.Common.Data.Errors;

public class Error
{
	public string Area { get; } = string.Empty;
	public string Message { get; } = string.Empty;
	public Color ErrorColor { get; } = new();

	private Error() { }

	public Error(string area, string message)
	{
		Area = area;
		Message = message;
	}

	public void Remove() =>
		Errors.Instance.RemoveError(this);
}
