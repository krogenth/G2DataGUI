using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Common;

[StructLayout(LayoutKind.Explicit, Size = 0x08)]
public struct Vector2
{
    [FieldOffset(0x00)] public float x;
    [FieldOffset(0x04)] public float z;
}