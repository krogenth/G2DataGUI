using G2DataGUI.IO.Streams;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Common;

[StructLayout(LayoutKind.Explicit, Size = 0x12)]
public struct FixedLengthName
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
    
    public string Name
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

            string name = Convert.ToChar(char1).ToString() + Convert.ToChar(char2).ToString() + Convert.ToChar(char3).ToString() + Convert.ToChar(char4).ToString()
            + Convert.ToChar(char5).ToString() + Convert.ToChar(char6).ToString() + Convert.ToChar(char7).ToString() + Convert.ToChar(char8).ToString()
            + Convert.ToChar(char9).ToString() + Convert.ToChar(char10).ToString() + Convert.ToChar(char11).ToString() + Convert.ToChar(char12).ToString()
            + Convert.ToChar(char13).ToString() + Convert.ToChar(char14).ToString() + Convert.ToChar(char15).ToString() + Convert.ToChar(char16).ToString()
            + Convert.ToChar(char17).ToString() + Convert.ToChar(char18).ToString();
            return name.TrimStart();
        }
        set
        {
            value = value.PadLeft(18, ' ');
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
        }
    }

    public int MaxLength
    {
        get => 18;
    }
}