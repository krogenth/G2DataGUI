using System;

namespace G2DataGUI.Events;

public class StyleThemeEventArgs(string style) : EventArgs
{
	public string Style { get; } = style;
}
