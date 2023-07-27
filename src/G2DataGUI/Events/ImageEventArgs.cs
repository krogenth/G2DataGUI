using Pfim;
using System;

namespace G2DataGUI.Events;

public class ImageEventArgs : EventArgs
{
	public ImageEventArgs(IImage image)
    {
        Image = image;
    }

	public IImage Image { get; }
}
