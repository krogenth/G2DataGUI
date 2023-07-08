using G2DataGUI.Common.Data.Common;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps;

[StructLayout(LayoutKind.Explicit, Size = 0x20)]
public struct MapEntry
{
    [FieldOffset(0x00)] public uint Id;

    [FieldOffset(0x04)] public Vector3 Position;
    [FieldOffset(0x10)] public float Direction;

    [FieldOffset(0x14)] public float Unknown1;
    [FieldOffset(0x18)] public int Unknown2;
    [FieldOffset(0x1C)] public int Unknown3;
}
