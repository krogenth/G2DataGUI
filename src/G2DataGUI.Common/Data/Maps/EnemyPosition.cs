using G2DataGUI.Common.Data.Common;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps;

    [StructLayout(LayoutKind.Explicit, Size = 0x68)]
    public struct EnemyPosition
    {
        [FieldOffset(0)] public ushort Index;
        [FieldOffset(2)] public short Unknown1;

        [FieldOffset(0x04)] public Vector2 Minimum;
        [FieldOffset(0x0C)] public Vector2 Maximum;
        [FieldOffset(0x14)] public Vector3 Position;

        [FieldOffset(0x20)] public Vector3 Unknown2;
        [FieldOffset(0x2C)] public Vector3 Unknown3;
        [FieldOffset(0x38)] public Vector3 Unknown4;
        [FieldOffset(0x44)] public Vector3 Unknown5;
        [FieldOffset(0x50)] public Vector3 Unknown6;
        [FieldOffset(0x5C)] public Vector3 Unknown7;
    }
