using Pfim;
using System;

namespace G2DataGUI.Events;

public class ImageEventArgs : EventArgs
{
    private readonly IImage _image;

    public ImageEventArgs(IImage image)
    {
        _image = image;
    }

    public IImage Image { get => _image; }
}
