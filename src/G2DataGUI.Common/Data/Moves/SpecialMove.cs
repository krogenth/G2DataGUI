using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Moves;

[StructLayout(LayoutKind.Explicit, Size = 0x04)]
public struct SpecialMove
{
    [FieldOffset(0x00)] public byte MoveOffset;
    [FieldOffset(0x01)] public byte StartingLevel;
    [FieldOffset(0x02)] public ushort StoryFlag;
}