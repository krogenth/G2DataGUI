using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Levelups;

[StructLayout(LayoutKind.Explicit, Size = 0x16)]
public class LevelupStats
{
    [FieldOffset(0x00)] public int experience;
    [FieldOffset(0x04)] public short increaseHP;
    [FieldOffset(0x06)] public short increaseMP;
    [FieldOffset(0x08)] public short increaseSP;
    [FieldOffset(0x0A)] public short increaseStrength;
    [FieldOffset(0x0C)] public short increaseAction;
    [FieldOffset(0x0E)] public short increaseMovement;
    [FieldOffset(0x10)] public short increaseMagic;
    [FieldOffset(0x12)] public short increaseMentality;
    [FieldOffset(0x14)] public short increaseSkillSlot;
}