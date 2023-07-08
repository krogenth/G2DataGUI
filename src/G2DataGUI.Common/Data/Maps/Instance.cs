using G2DataGUI.Common.Data.Common;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps;

[StructLayout(LayoutKind.Explicit, Size = 0x30)]
public struct Instance
{
    [FieldOffset(0x00)] public ushort Id;
    [FieldOffset(0x02)] public ushort Index;
    [FieldOffset(0x04)] public short Unknown1;
    [FieldOffset(0x06)] public byte Translation;
    [FieldOffset(0x07)] public byte Unknown2;

    [FieldOffset(0x08)] public Vector3 Position;
    [FieldOffset(0x14)] public Vector3 Angle;
    [FieldOffset(0x20)] public Vector3 C;

    [FieldOffset(0x2C)] public float Rotation;
}
