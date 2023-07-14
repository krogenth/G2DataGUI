using CommunityToolkit.Diagnostics;
using Pfim;

namespace G2DataGUI.Common.Extensions;
public static class IImageExtensions
{
	public static uint GUID(this IImage image)
	{
		Guard.IsAssignableToType(image, typeof(Dds));
		var dds = image as Dds;
		return dds.Header.Reserved1[0];
	}
}
