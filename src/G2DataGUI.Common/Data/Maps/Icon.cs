using G2DataGUI.IO.Streams;
using G2DataGUI.Common.Data.Common;
using System.IO;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps
{
    [StructLayout(LayoutKind.Explicit, Size = 0x20)]
    public struct Icon
    {
        [FieldOffset(0x00)] public byte Id;

        [FieldOffset(0x01)] public byte Unknown1;
        [FieldOffset(0x02)] public short Unknown2;

        [FieldOffset(0x04)] public Vector3 Position;

        [FieldOffset(0x10)] public float Unknown3;

        [FieldOffset(0x14)] public float YAngle;

        [FieldOffset(0x18)] public short Item1;
        [FieldOffset(0x1A)] public short Item2;
        [FieldOffset(0x1C)] public short Item3;

        [FieldOffset(0x1E)] public short Flag;
    }
}