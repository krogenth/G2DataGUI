using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class Skillbook : BaseContainer
{
    private SkillbookSkill[] _skills = new SkillbookSkill[6];

    public static Skillbook ReadSkillbook(Stream reader)
    {
        Skillbook skillbook = new Skillbook();
        for (int index = 0; index < skillbook._skills.Length; index++)
        {
            SkillbookSkill? skillbookSkill = reader.ReadStruct<SkillbookSkill>();
            if (skillbookSkill == null) return null;
            skillbook._skills[index] = skillbookSkill.Value;
        }

        return skillbook;
    }

    public void WriteSkillbook(Stream writer)
    {
        foreach (var skill in _skills)
        {
            writer.WriteStruct(skill);
        }
    }

    public int NumSkills { get => _skills.Length; }

    public byte Skill1Offset { get => _skills[0].SkillOffset; set => _skills[0].SkillOffset = value; }
    public byte Skill1StartingLevel { get => _skills[0].StartingLevel; set => _skills[0].StartingLevel = value; }
    public byte Skill1RequiredLevel { get => _skills[0].RequiredLevel; set => _skills[0].RequiredLevel = value; }

    public byte Skill2Offset { get => _skills[1].SkillOffset; set => _skills[1].SkillOffset = value; }
    public byte Skill2StartingLevel { get => _skills[1].StartingLevel; set => _skills[1].StartingLevel = value; }
    public byte Skill2RequiredLevel { get => _skills[1].RequiredLevel; set => _skills[1].RequiredLevel = value; }

    public byte Skill3Offset { get => _skills[2].SkillOffset; set => _skills[2].SkillOffset = value; }
    public byte Skill3StartingLevel { get => _skills[2].StartingLevel; set => _skills[2].StartingLevel = value; }
    public byte Skill3RequiredLevel { get => _skills[2].RequiredLevel; set => _skills[2].RequiredLevel = value; }

    public byte Skill4Offset { get => _skills[3].SkillOffset; set => _skills[3].SkillOffset = value; }
    public byte Skill4StartingLevel { get => _skills[3].StartingLevel; set => _skills[3].StartingLevel = value; }
    public byte Skill4RequiredLevel { get => _skills[3].RequiredLevel; set => _skills[3].RequiredLevel = value; }

    public byte Skill5Offset { get => _skills[4].SkillOffset; set => _skills[4].SkillOffset = value; }
    public byte Skill5StartingLevel { get => _skills[4].StartingLevel; set => _skills[4].StartingLevel = value; }
    public byte Skill5RequiredLevel { get => _skills[4].RequiredLevel; set => _skills[4].RequiredLevel = value; }

    public byte Skill6Offset { get => _skills[5].SkillOffset; set => _skills[5].SkillOffset = value; }
    public byte Skill6StartingLevel { get => _skills[5].StartingLevel; set => _skills[5].StartingLevel = value; }
    public byte Skill6RequiredLevel { get => _skills[5].RequiredLevel; set => _skills[5].RequiredLevel = value; }
}