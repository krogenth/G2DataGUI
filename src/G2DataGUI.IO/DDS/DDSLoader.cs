using Pfim;

namespace G2DataGUI.IO.DDS;

public static class DDSLoader
{
    public static IImage LoadDDSFile(string file)
    {
        if (file == null || file.Length == 0) return null;

        var image = Pfimage.FromFile(file);

        return image;
    }
}