using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Models.Common;

[StructLayout(LayoutKind.Explicit)]
public struct NJSColor
{
	[FieldOffset(0)] public uint Color;
	[FieldOffset(0)] public NJSTexture Texture;
	[FieldOffset(0)] public NJSBGRA ARGB;
}
