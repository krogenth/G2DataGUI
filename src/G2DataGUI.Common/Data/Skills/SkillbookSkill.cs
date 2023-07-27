using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class SkillbookSkill
{
    public byte SkillOffset { get; set; }
    public byte StartingLevel { get; set; }
    public byte RequiredLevel { get; set; }
    public byte Unknown1 { get; set; }

	public static string CSVHeader =>
		$"Skill,Starting Level,Required Level,Unknown #1";

    public static SkillbookSkill ReadSkillbookSkill(Stream reader)
    {
        SkillbookSkill skill = new()
        {
            SkillOffset = reader.ReadRawByte(),
            StartingLevel = reader.ReadRawByte(),
            RequiredLevel = reader.ReadRawByte(),
            Unknown1 = reader.ReadRawByte(),
        };

        return skill;
    }

    public void WriteSkillbookSkill(Stream writer)
    {
        writer.WriteRawByte(SkillOffset);
        writer.WriteRawByte(StartingLevel);
        writer.WriteRawByte(RequiredLevel);
        writer.WriteRawByte(Unknown1);
    }

	public void GenerateCSV(StreamWriter writer)
	{
		writer.Write(
			$"{Skills.Instance.GameSkills[SkillOffset].Name}," +
			$"{StartingLevel}," +
			$"{RequiredLevel}," +
			$"{Unknown1}");
	}
}
