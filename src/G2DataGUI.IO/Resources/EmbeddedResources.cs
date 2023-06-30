using System;
using System.IO;
using System.Linq;
using System.Reflection;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.IO.Resources;

public static class EmbeddedResources
{
    private readonly static Assembly ResourceAssembly;

    static EmbeddedResources()
    {
        ResourceAssembly = Assembly.GetAssembly(typeof(EmbeddedResources));
    }

    public static byte[] Read(string filename)
    {
        var (assembly, path) = ResolveManifestPath(filename);

        return Read(assembly, path);
    }

    public static byte[] Read(Assembly assembly, string filename)
    {
        using (var stream = GetStream(assembly, filename))
        {
            if (stream == null)
            {
                return null;
            }

            return stream.StreamToBytesArray();
        }
    }

    public static string ReadAllText(string filename)
    {
        var (assembly, path) = ResolveManifestPath(filename);

        return ReadAllText(assembly, path);
    }

    public static string ReadAllText(Assembly assembly, string filename)
    {
        using (var stream = GetStream(assembly, filename))
        {
            if (stream == null)
            {
                return null;
            }

            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }

    public static Stream GetStream(string filename)
    {
        var (assembly, path) = ResolveManifestPath(filename);

        return GetStream(assembly, path);
    }

    public static Stream GetStream(Assembly assembly, string filename)
    {
        var namespace_ = assembly.GetName().Name;
        var manifestUri = namespace_ + "." + filename.Replace('/', '.');

        var stream = assembly.GetManifestResourceStream(manifestUri);

        return stream;
    }

    public static string[] GetAllAvailableResources(string path, string ext = "")
    {
        return ResolveManifestPath(path).Item1.GetManifestResourceNames()
            .Where(r => r.EndsWith(ext))
            .ToArray();
    }

    private static (Assembly, string) ResolveManifestPath(string filename)
    {
        var segments = filename.Split('/', 2, StringSplitOptions.RemoveEmptyEntries);

        if (segments.Length >= 2)
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assembly.GetName().Name == segments[0])
                {
                    return (assembly, segments[1]);
                }
            }
        }

        return (ResourceAssembly, filename);
    }
}