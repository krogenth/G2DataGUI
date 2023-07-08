using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps;

[StructLayout(LayoutKind.Explicit, Size = 0xC8)]
public struct Header
{
    [FieldOffset(0x00)] public uint HeaderLength;
    [FieldOffset(0x04)] public uint Unknown1;

    [FieldOffset(0x08)] public uint NumMapEntries;
    [FieldOffset(0x0C)] public uint OffsetMapEntries;

    [FieldOffset(0x10)] public uint Unknown2;
    [FieldOffset(0x14)] public uint Unknown3;

    [FieldOffset(0x18)] public uint NumInstances;
    [FieldOffset(0x1C)] public uint OffsetInstances;

    [FieldOffset(0x20)] public uint NumHTA;
    [FieldOffset(0x24)] public uint OffsetHTA;

    [FieldOffset(0x28)] public uint NumScripts;
    [FieldOffset(0x2C)] public uint OffsetScripts;

    [FieldOffset(0x30)] public uint Unknown4;
    [FieldOffset(0x34)] public uint Unknown5;

    [FieldOffset(0x38)] public uint NumEnemyPos;
    [FieldOffset(0x3C)] public uint OffsetEnemyPos;

    [FieldOffset(0x40)] public uint Unknown6;
    [FieldOffset(0x44)] public uint Unknown7;

    [FieldOffset(0x48)] public uint NumEnemyGroups;
    [FieldOffset(0x4C)] public uint OffsetEnemyGroups;

    [FieldOffset(0x50)] public uint NumMOS;
    [FieldOffset(0x54)] public uint OffsetMOS;

    [FieldOffset(0x58)] public uint Unknown8;
    [FieldOffset(0x5C)] public uint Unknown9;

    [FieldOffset(0x60)] public uint Unknown10;
    [FieldOffset(0x64)] public uint Unknown11;

    [FieldOffset(0x68)] public uint Unknown12;
    [FieldOffset(0x6C)] public uint Unknown13;

    [FieldOffset(0x70)] public uint Unknown14;
    [FieldOffset(0x74)] public uint Unknown15;

    [FieldOffset(0x78)] public uint LengthDialogue;
    [FieldOffset(0x7C)] public uint OffsetDialogue;

    [FieldOffset(0x80)] public uint Unknown16;
    [FieldOffset(0x84)] public uint Unknown17;

    [FieldOffset(0x88)] public uint Unknown18;
    [FieldOffset(0x8C)] public uint Unknown19;

    [FieldOffset(0x90)] public uint Unknown20;
    [FieldOffset(0x94)] public uint Unknown21;

    [FieldOffset(0x98)] public uint NumIcons;
    [FieldOffset(0x9C)] public uint OffsetIcons;

    [FieldOffset(0xA0)] public uint Unknown22;
    [FieldOffset(0xA4)] public uint Unknown23;

    [FieldOffset(0xA8)] public uint LengthShop;
    [FieldOffset(0xAC)] public uint OffsetShop;

    [FieldOffset(0xB0)] public uint Unknown24;
    [FieldOffset(0xB4)] public uint Unkown25;

    [FieldOffset(0xB8)] public uint Unknown26;
    [FieldOffset(0xBC)] public uint Unknown27;

    [FieldOffset(0xC0)] public uint Unknown28;
    [FieldOffset(0xC4)] public uint Unknown29;
}
