using System;

namespace G2DataGUI.Events;

public class StyleThemeEventArgs : EventArgs
{
	public StyleThemeEventArgs(string style)
    {
        Style = style;
    }

	public string Style { get; }
}