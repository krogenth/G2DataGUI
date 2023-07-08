using Pfim;
using Microsoft.Toolkit.Diagnostics;

namespace G2DataGUI.IO.DDS;

public static class DDSLoader
{
    public static IImage LoadDDSFile(string file)
    {
        Guard.IsNotNullOrEmpty(file, nameof(file));
        var image = Pfimage.FromFile(file);
        return image;
    }
}