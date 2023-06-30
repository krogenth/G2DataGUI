using G2DataGUI.IO.Streams;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Common;

[StructLayout(LayoutKind.Explicit, Size = 0x28)]
public struct FixedLengthDescription
{
    [FieldOffset(0x00)] private byte char1;
    [FieldOffset(0x01)] private byte char2;
    [FieldOffset(0x02)] private byte char3;
    [FieldOffset(0x03)] private byte char4;
    [FieldOffset(0x04)] private byte char5;
    [FieldOffset(0x05)] private byte char6;
    [FieldOffset(0x06)] private byte char7;
    [FieldOffset(0x07)] private byte char8;
    [FieldOffset(0x08)] private byte char9;
    [FieldOffset(0x09)] private byte char10;
    [FieldOffset(0x0A)] private byte char11;
    [FieldOffset(0x0B)] private byte char12;
    [FieldOffset(0x0C)] private byte char13;
    [FieldOffset(0x0D)] private byte char14;
    [FieldOffset(0x0E)] private byte char15;
    [FieldOffset(0x0F)] private byte char16;
    [FieldOffset(0x10)] private byte char17;
    [FieldOffset(0x11)] private byte char18;
    [FieldOffset(0x12)] private byte char19;
    [FieldOffset(0x13)] private byte char20;
    [FieldOffset(0x14)] private byte char21;
    [FieldOffset(0x15)] private byte char22;
    [FieldOffset(0x16)] private byte char23;
    [FieldOffset(0x17)] private byte char24;
    [FieldOffset(0x18)] private byte char25;
    [FieldOffset(0x19)] private byte char26;
    [FieldOffset(0x1A)] private byte char27;
    [FieldOffset(0x1B)] private byte char28;
    [FieldOffset(0x1C)] private byte char29;
    [FieldOffset(0x1D)] private byte char30;
    [FieldOffset(0x1E)] private byte char31;
    [FieldOffset(0x1F)] private byte char32;
    [FieldOffset(0x20)] private byte char33;
    [FieldOffset(0x21)] private byte char34;
    [FieldOffset(0x22)] private byte char35;
    [FieldOffset(0x23)] private byte char36;
    [FieldOffset(0x24)] private byte char37;
    [FieldOffset(0x25)] private byte char38;
    [FieldOffset(0x26)] private byte char39;
    [FieldOffset(0x27)] private byte char40;

    public string Description
    {
        get
        {
            if (char1 == '\0') char1 = Convert.ToByte(' ');
            if (char2 == '\0') char2 = Convert.ToByte(' ');
            if (char3 == '\0') char3 = Convert.ToByte(' ');
            if (char4 == '\0') char4 = Convert.ToByte(' ');
            if (char5 == '\0') char5 = Convert.ToByte(' ');
            if (char6 == '\0') char6 = Convert.ToByte(' ');
            if (char7 == '\0') char7 = Convert.ToByte(' ');
            if (char8 == '\0') char8 = Convert.ToByte(' ');
            if (char9 == '\0') char9 = Convert.ToByte(' ');
            if (char10 == '\0') char10 = Convert.ToByte(' ');
            if (char11 == '\0') char11 = Convert.ToByte(' ');
            if (char12 == '\0') char12 = Convert.ToByte(' ');
            if (char13 == '\0') char13 = Convert.ToByte(' ');
            if (char14 == '\0') char14 = Convert.ToByte(' ');
            if (char15 == '\0') char15 = Convert.ToByte(' ');
            if (char16 == '\0') char16 = Convert.ToByte(' ');
            if (char17 == '\0') char17 = Convert.ToByte(' ');
            if (char18 == '\0') char18 = Convert.ToByte(' ');
            if (char19 == '\0') char19 = Convert.ToByte(' ');
            if (char20 == '\0') char20 = Convert.ToByte(' ');
            if (char21 == '\0') char21 = Convert.ToByte(' ');
            if (char22 == '\0') char22 = Convert.ToByte(' ');
            if (char23 == '\0') char23 = Convert.ToByte(' ');
            if (char24 == '\0') char24 = Convert.ToByte(' ');
            if (char25 == '\0') char25 = Convert.ToByte(' ');
            if (char26 == '\0') char26 = Convert.ToByte(' ');
            if (char27 == '\0') char27 = Convert.ToByte(' ');
            if (char28 == '\0') char28 = Convert.ToByte(' ');
            if (char29 == '\0') char29 = Convert.ToByte(' ');
            if (char30 == '\0') char30 = Convert.ToByte(' ');
            if (char31 == '\0') char31 = Convert.ToByte(' ');
            if (char32 == '\0') char32 = Convert.ToByte(' ');
            if (char33 == '\0') char33 = Convert.ToByte(' ');
            if (char34 == '\0') char34 = Convert.ToByte(' ');
            if (char35 == '\0') char35 = Convert.ToByte(' ');
            if (char36 == '\0') char36 = Convert.ToByte(' ');
            if (char37 == '\0') char37 = Convert.ToByte(' ');
            if (char38 == '\0') char38 = Convert.ToByte(' ');
            if (char39 == '\0') char39 = Convert.ToByte(' ');
            if (char40 == '\0') char40 = Convert.ToByte(' ');

            string description = Convert.ToChar(char1).ToString() + Convert.ToChar(char2).ToString() + Convert.ToChar(char3).ToString() + Convert.ToChar(char4).ToString()
            + Convert.ToChar(char5).ToString() + Convert.ToChar(char6).ToString() + Convert.ToChar(char7).ToString() + Convert.ToChar(char8).ToString()
            + Convert.ToChar(char9).ToString() + Convert.ToChar(char10).ToString() + Convert.ToChar(char11).ToString() + Convert.ToChar(char12).ToString()
            + Convert.ToChar(char13).ToString() + Convert.ToChar(char14).ToString() + Convert.ToChar(char15).ToString() + Convert.ToChar(char16).ToString()
            + Convert.ToChar(char17).ToString() + Convert.ToChar(char18).ToString() + Convert.ToChar(char19).ToString() + Convert.ToChar(char20).ToString()
            + Convert.ToChar(char21).ToString() + Convert.ToChar(char22).ToString() + Convert.ToChar(char23).ToString() + Convert.ToChar(char24).ToString()
            + Convert.ToChar(char25).ToString() + Convert.ToChar(char26).ToString() + Convert.ToChar(char27).ToString() + Convert.ToChar(char28).ToString()
            + Convert.ToChar(char29).ToString() + Convert.ToChar(char30).ToString() + Convert.ToChar(char31).ToString() + Convert.ToChar(char32).ToString()
            + Convert.ToChar(char33).ToString() + Convert.ToChar(char34).ToString() + Convert.ToChar(char35).ToString() + Convert.ToChar(char36).ToString()
            + Convert.ToChar(char37).ToString() + Convert.ToChar(char38).ToString() + Convert.ToChar(char39).ToString() + Convert.ToChar(char40).ToString();
            return description.TrimEnd();
        }
        set
        {
            value = value.PadRight(40, ' ');
            char1 = Convert.ToByte(value[0]);
            char2 = Convert.ToByte(value[1]);
            char3 = Convert.ToByte(value[2]);
            char4 = Convert.ToByte(value[3]);
            char5 = Convert.ToByte(value[4]);
            char6 = Convert.ToByte(value[5]);
            char7 = Convert.ToByte(value[6]);
            char8 = Convert.ToByte(value[7]);
            char9 = Convert.ToByte(value[8]);
            char10 = Convert.ToByte(value[9]);
            char11 = Convert.ToByte(value[10]);
            char12 = Convert.ToByte(value[11]);
            char13 = Convert.ToByte(value[12]);
            char14 = Convert.ToByte(value[13]);
            char15 = Convert.ToByte(value[14]);
            char16 = Convert.ToByte(value[15]);
            char17 = Convert.ToByte(value[16]);
            char18 = Convert.ToByte(value[17]);
            char19 = Convert.ToByte(value[18]);
            char20 = Convert.ToByte(value[19]);
            char21 = Convert.ToByte(value[20]);
            char22 = Convert.ToByte(value[21]);
            char23 = Convert.ToByte(value[22]);
            char24 = Convert.ToByte(value[23]);
            char25 = Convert.ToByte(value[24]);
            char26 = Convert.ToByte(value[25]);
            char27 = Convert.ToByte(value[26]);
            char28 = Convert.ToByte(value[27]);
            char29 = Convert.ToByte(value[28]);
            char30 = Convert.ToByte(value[29]);
            char31 = Convert.ToByte(value[30]);
            char32 = Convert.ToByte(value[31]);
            char33 = Convert.ToByte(value[32]);
            char34 = Convert.ToByte(value[33]);
            char35 = Convert.ToByte(value[34]);
            char36 = Convert.ToByte(value[35]);
            char37 = Convert.ToByte(value[36]);
            char38 = Convert.ToByte(value[37]);
            char39 = Convert.ToByte(value[38]);
            char40 = Convert.ToByte(value[39]);
        }
    }

    public int MaxLength
    {
        get => 40;
    }
}