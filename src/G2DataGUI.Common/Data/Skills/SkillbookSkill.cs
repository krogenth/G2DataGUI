using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Skills;

[StructLayout(LayoutKind.Explicit, Size = 0x04)]
public struct SkillbookSkill
{
    [FieldOffset(0x00)] public byte SkillOffset;
    [FieldOffset(0x01)] public byte StartingLevel;
    [FieldOffset(0x02)] public byte RequiredLevel;
    [FieldOffset(0x03)] public byte Unknown1;
}