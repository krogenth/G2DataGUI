using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class SkillbookSkill
{
    public byte SkillOffset { get; set; }
    public byte StartingLevel { get; set; }
    public byte RequiredLevel { get; set; }
    public byte Unknown1 { get; set; }

    public static SkillbookSkill ReadSkillbookSkill(Stream reader)
    {
        SkillbookSkill skill = new SkillbookSkill();
        skill.SkillOffset = reader.ReadRawByte();
        skill.StartingLevel = reader.ReadRawByte();
        skill.RequiredLevel = reader.ReadRawByte();
        skill.Unknown1 = reader.ReadRawByte();

        return skill;
    }

    public void WriteSkillbookSkill(Stream writer)
    {
        writer.WriteRawByte(SkillOffset);
        writer.WriteRawByte(StartingLevel);
        writer.WriteRawByte(RequiredLevel);
        writer.WriteRawByte(Unknown1);
    }
}