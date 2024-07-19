using Pfim;
using System;

namespace G2DataGUI.Events;

public class ImageEventArgs(IImage image) : EventArgs
{
	public IImage Image { get; } = image;
}
