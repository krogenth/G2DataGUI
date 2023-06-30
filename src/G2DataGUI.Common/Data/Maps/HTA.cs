using G2DataGUI.IO.Streams;
using G2DataGUI.Common.Data.Common;
using System.IO;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps
{
    [StructLayout(LayoutKind.Explicit, Size = 0x48)]
    public struct HTA
    {
        [FieldOffset(0x00)] public byte Shape;
        [FieldOffset(0x01)] public byte Type;
        [FieldOffset(0x02)] public byte Trigger;

        [FieldOffset(0x03)] public byte Unknown1;
        [FieldOffset(0x04)] public int Unknown2;

        [FieldOffset(0x08)] public byte Attribute;

        [FieldOffset(0x09)] public byte Unknown3;
        [FieldOffset(0x0A)] public short Unknown4;
        [FieldOffset(0x0C)] public int Unknown5;

        [FieldOffset(0x10)] public Vector3 Minimum;
        [FieldOffset(0x1C)] public Vector3 Maximum;

        [FieldOffset(0x28)] public Vector3 Unknown6;
        [FieldOffset(0x34)] public int Unknown7;
        [FieldOffset(0x38)] public int Unknown8;
        [FieldOffset(0x3C)] public int Unknown9;
        [FieldOffset(0x40)] public int Unknown10;
        [FieldOffset(0x44)] public int Unknown11;
    }
}