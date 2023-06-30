using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Common;

[StructLayout(LayoutKind.Explicit, Size = 0x0C)]
public struct Vector3
{
    [FieldOffset(0x00)] public float x;
    [FieldOffset(0x04)] public float y;
    [FieldOffset(0x08)] public float z;
}