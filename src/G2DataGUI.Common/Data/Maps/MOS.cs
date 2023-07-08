using G2DataGUI.Common.Data.Common;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps;

[StructLayout(LayoutKind.Explicit, Size = 0x20)]
public struct MOS
{
    [FieldOffset(0x00)] public ushort Id;
    [FieldOffset(0x02)] public ushort Index;
    [FieldOffset(0x04)] public int Unknown1;

    [FieldOffset(0x08)] public Vector3 Position;

    [FieldOffset(0x0E)] public int Unknown2;
    [FieldOffset(0x12)] public int Unknown3;
    [FieldOffset(0x16)] public int Unknown4;
    [FieldOffset(0x1A)] public int Unknown5;
    [FieldOffset(0x1E)] public short Unknown6;
}
