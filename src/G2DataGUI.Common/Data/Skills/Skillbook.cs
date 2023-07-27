using G2DataGUI.Common.Data.Common;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class Skillbook : BaseContainer
{
    public SkillbookSkill Skill1 { get; private set; }
    public SkillbookSkill Skill2 { get; private set; }
    public SkillbookSkill Skill3 { get; private set; }
    public SkillbookSkill Skill4 { get; private set; }
    public SkillbookSkill Skill5 { get; private set; }
    public SkillbookSkill Skill6 { get; private set; }

	public static string CSVHeader =>
		$"{SkillbookSkill.CSVHeader},{SkillbookSkill.CSVHeader},{SkillbookSkill.CSVHeader}," +
		$"{SkillbookSkill.CSVHeader},{SkillbookSkill.CSVHeader},{SkillbookSkill.CSVHeader}";

    public static Skillbook ReadSkillbook(Stream reader)
    {
        Skillbook skillbook = new()
        {
            Skill1 = SkillbookSkill.ReadSkillbookSkill(reader),
            Skill2 = SkillbookSkill.ReadSkillbookSkill(reader),
            Skill3 = SkillbookSkill.ReadSkillbookSkill(reader),
            Skill4 = SkillbookSkill.ReadSkillbookSkill(reader),
            Skill5 = SkillbookSkill.ReadSkillbookSkill(reader),
            Skill6 = SkillbookSkill.ReadSkillbookSkill(reader),
        };

        return skillbook;
    }

    public void WriteSkillbook(Stream writer)
    {
        Skill1.WriteSkillbookSkill(writer);
        Skill2.WriteSkillbookSkill(writer);
        Skill3.WriteSkillbookSkill(writer);
        Skill4.WriteSkillbookSkill(writer);
        Skill5.WriteSkillbookSkill(writer);
        Skill6.WriteSkillbookSkill(writer);
    }

	public void GenerateCSV(StreamWriter writer)
	{
		Skill1.GenerateCSV(writer);
		writer.Write(',');
		Skill2.GenerateCSV(writer);
		writer.Write(',');
		Skill3.GenerateCSV(writer);
		writer.Write(',');
		Skill4.GenerateCSV(writer);
		writer.Write(',');
		Skill5.GenerateCSV(writer);
		writer.Write(',');
		Skill6.GenerateCSV(writer);
		writer.WriteLine();
	}
}
