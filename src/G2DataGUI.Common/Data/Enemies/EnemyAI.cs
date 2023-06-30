using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Enemies;

[StructLayout(LayoutKind.Explicit, Size = 0x06)]
public struct EnemyAI
{
    [FieldOffset(0x00)] public byte aiType;
    [FieldOffset(0x01)] public byte move1Chance;
    [FieldOffset(0x02)] public byte move2Chance;
    [FieldOffset(0x03)] public byte move3Chance;
    [FieldOffset(0x04)] public byte move4Chance;
    [FieldOffset(0x05)] public byte move5Chance;
}