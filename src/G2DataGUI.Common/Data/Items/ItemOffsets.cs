using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Items;

[StructLayout(LayoutKind.Explicit, Size = 0x08)]
internal struct ItemOffsets
{
    [FieldOffset(0x00)] public int EquipmentOffset;
    [FieldOffset(0x04)] public int UsableOffset;
}