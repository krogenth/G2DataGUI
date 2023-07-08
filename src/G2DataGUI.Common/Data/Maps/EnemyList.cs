using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Maps;

[StructLayout(LayoutKind.Explicit, Size = 0x38)]
public struct EnemyList
{
    [FieldOffset(0x00)] public EnemyType Enemy1;
    [FieldOffset(0x0E)] public EnemyType Enemy2;
    [FieldOffset(0x1C)] public EnemyType Enemy3;
    [FieldOffset(0x2A)] public EnemyType Enemy4;
}

[StructLayout(LayoutKind.Explicit, Size = 0x0E)]
public struct EnemyType
{
    [FieldOffset(0x00)] public ushort Index;
    [FieldOffset(0x02)] public short NumberOfEnemy;
    [FieldOffset(0x04)] public short EnemyOffset;
    [FieldOffset(0x06)] public short Unknown1;
    [FieldOffset(0x08)] public short Unknown2;
    [FieldOffset(0x0A)] public short Unknown3;
    [FieldOffset(0x0C)] public short Unknown4;
}
