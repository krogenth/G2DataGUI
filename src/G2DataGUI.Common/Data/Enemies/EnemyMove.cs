using System.Runtime.InteropServices;
using G2DataGUI.Common.Data.Common;

namespace G2DataGUI.Common.Data.Enemies;

[StructLayout(LayoutKind.Explicit, Size = 0x3C)]
public struct EnemyMove
{
    [FieldOffset(0)] public FixedLengthName name;
    [FieldOffset(0x12)] public EnemyMoveStats stats;
}