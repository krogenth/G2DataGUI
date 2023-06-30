using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
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

    public static Skillbook ReadSkillbook(Stream reader)
    {
        Skillbook skillbook = new Skillbook();
        skillbook.Skill1 = SkillbookSkill.ReadSkillbookSkill(reader);
        skillbook.Skill2 = SkillbookSkill.ReadSkillbookSkill(reader);
        skillbook.Skill3 = SkillbookSkill.ReadSkillbookSkill(reader);
        skillbook.Skill4 = SkillbookSkill.ReadSkillbookSkill(reader);
        skillbook.Skill5 = SkillbookSkill.ReadSkillbookSkill(reader);
        skillbook.Skill6 = SkillbookSkill.ReadSkillbookSkill(reader);

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
}