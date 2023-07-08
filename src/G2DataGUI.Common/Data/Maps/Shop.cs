using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps;

[StructLayout(LayoutKind.Explicit, Size = 0xA4)]
public struct Shop
{
    [FieldOffset(0x00)] public ushort MapID;
    [FieldOffset(0x02)] public ushort Categories;
    [FieldOffset(0x04)] public ShopSection Weapons;
    [FieldOffset(0x24)] public ShopSection Armors;
    [FieldOffset(0x44)] public ShopSection Jewelry;
    [FieldOffset(0x64)] public ShopSection Items;
    [FieldOffset(0x84)] public ShopSection Regionals;
}
