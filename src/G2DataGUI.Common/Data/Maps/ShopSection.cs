using System;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps
{
    [StructLayout(LayoutKind.Explicit, Size = 0x20)]
    public struct ShopSection
    {
        [FieldOffset(0x00)] public ShopSectionTitle Title;
        [FieldOffset(0x08)] public ShopItemList Items;
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x08)]
    public struct ShopSectionTitle
    {
        [FieldOffset(0x00)] private byte char1;
        [FieldOffset(0x01)] private byte char2;
        [FieldOffset(0x02)] private byte char3;
        [FieldOffset(0x03)] private byte char4;
        [FieldOffset(0x04)] private byte char5;
        [FieldOffset(0x05)] private byte char6;
        [FieldOffset(0x06)] private byte char7;
        [FieldOffset(0x07)] private byte char8;

        public string Title
        {
            get => Convert.ToChar(char1).ToString() + Convert.ToChar(char2).ToString() + Convert.ToChar(char3).ToString() + Convert.ToChar(char4).ToString()
                + Convert.ToChar(char5).ToString() + Convert.ToChar(char6).ToString() + Convert.ToChar(char7).ToString() + Convert.ToChar(char8).ToString();
        }
    }

    [StructLayout(LayoutKind.Explicit, Size = 0x18)]
    public struct ShopItemList
    {
        [FieldOffset(0x00)] public ushort Item1;
        [FieldOffset(0x02)] public ushort Item2;
        [FieldOffset(0x04)] public ushort Item3;
        [FieldOffset(0x06)] public ushort Item4;
        [FieldOffset(0x08)] public ushort Item5;
        [FieldOffset(0x0A)] public ushort Item6;
        [FieldOffset(0x0C)] public ushort Item7;
        [FieldOffset(0x0E)] public ushort Item8;
        [FieldOffset(0x10)] public ushort Item9;
        [FieldOffset(0x12)] public ushort Item10;
        [FieldOffset(0x14)] public ushort Item11;
        [FieldOffset(0x16)] public ushort Item12;
    }
}