using System;

namespace G2DataGUI.Events;

public class StyleThemeEventArgs : EventArgs
{
    private readonly string _style;

    public StyleThemeEventArgs(string style)
    {
        _style = style;
    }

    public string Style { get => _style; }
}
