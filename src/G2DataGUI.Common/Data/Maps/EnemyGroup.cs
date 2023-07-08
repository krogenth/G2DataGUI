using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps;

[StructLayout(LayoutKind.Explicit, Size = 0x78)]
public struct EnemyGroup
{
    [FieldOffset(0x00)] public ushort Index;
    [FieldOffset(0x02)] public EnemyFirstUnknown Unknown1;
    [FieldOffset(0x18)] public EnemyList Enemies;
    [FieldOffset(0x50)] public EnemySecondUnknown Unknown2;
}

[StructLayout(LayoutKind.Explicit, Size = 0x16)]
public struct EnemyFirstUnknown
{
    [FieldOffset(0x00)] public short Unknown1;
    [FieldOffset(0x02)] public short Unknown2;
    [FieldOffset(0x04)] public short Unknown3;
    [FieldOffset(0x06)] public short Unknown4;
    [FieldOffset(0x08)] public short Unknown5;
    [FieldOffset(0x0A)] public float Unknown6;
    [FieldOffset(0x0E)] public short Unknown7;
    [FieldOffset(0x10)] public short Unknown8;
    [FieldOffset(0x12)] public short Unknown9;
    [FieldOffset(0x14)] public short Unknown10;
}

[StructLayout(LayoutKind.Explicit, Size = 0x28)]
public struct EnemySecondUnknown
{
    [FieldOffset(0x00)] public short Unknown1;
    [FieldOffset(0x02)] public short Unknown2;
    [FieldOffset(0x04)] public short Unknown3;
    [FieldOffset(0x06)] public short Unknown4;
    [FieldOffset(0x08)] public short Unknown5;
    [FieldOffset(0x0A)] public short Unknown6;
    [FieldOffset(0x0C)] public short Unknown7;
    [FieldOffset(0x0E)] public short Unknown8;
    [FieldOffset(0x10)] public short Unknown9;
    [FieldOffset(0x12)] public short Unknown10;
    [FieldOffset(0x14)] public short Unknown11;
    [FieldOffset(0x16)] public short Unknown12;
    [FieldOffset(0x18)] public short Unknown13;
    [FieldOffset(0x1A)] public short Unknown14;
    [FieldOffset(0x1C)] public short Unknown15;
    [FieldOffset(0x1E)] public short Unknown16;
    [FieldOffset(0x20)] public short Unknown17;
    [FieldOffset(0x22)] public short Unknown18;
    [FieldOffset(0x24)] public short Unknown19;
    [FieldOffset(0x26)] public short Unknown20;
}
