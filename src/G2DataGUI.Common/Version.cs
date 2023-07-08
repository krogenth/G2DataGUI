using System.IO;

namespace G2DataGUI.Common;

public class Version
{
    public static Version Instance { get; } = new();
    public bool IsHDVersion { get; private set; } = true;

    private Version()
    {
        if (!Directory.Exists("content"))
        {
            IsHDVersion = false;
        }
    }

    public string RootDataDirectory
    {
        get => IsHDVersion ? ".\\content\\data\\afs\\" : ".\\data\\afs\\";
    }
}