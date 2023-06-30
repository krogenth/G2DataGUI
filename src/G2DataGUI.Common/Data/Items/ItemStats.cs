using G2DataGUI.IO.Streams;
using System.IO;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Items;

[StructLayout(LayoutKind.Explicit, Size = 0x0A)]
public struct ItemStats
{
    [FieldOffset(0x00)] public byte EntryType;
    [FieldOffset(0x01)] public byte Unknown1;
    [FieldOffset(0x02)] public byte Unknown2;
    [FieldOffset(0x03)] public byte Unknown3;
    [FieldOffset(0x04)] public byte Icon;
    [FieldOffset(0x05)] public byte Unknown4;
    [FieldOffset(0x06)] public int Price;
}