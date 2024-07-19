using CommunityToolkit.Diagnostics;
using G2DataGUI.IO.Streams;
using Pfim;

namespace G2DataGUI.IO.DDS;

public static class DDSLoader
{
	private const uint GuidOffset = 0x20;

    public static IImage LoadDDSFile(string file)
    {
        Guard.IsNotNullOrEmpty(file);
        var image = Pfimage.FromFile(file);
        return image;
    }

	public static uint GetGuid(Stream reader)
	{
		var position = reader.Position;
		reader.Seek(GuidOffset, SeekOrigin.Begin);
		var guid = reader.ReadRawUInt();
		reader.Seek(position, SeekOrigin.Begin);
		return guid;
	}
}
