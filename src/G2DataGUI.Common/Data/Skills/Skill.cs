using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class Skill : BaseContainer
{
    private FixedLengthName _name;
    public SkillStats Stats { get; private set; }
    private FixedLengthDescription _description;

    public string Name { get => _name.Name; set { _name.Name = value; NotifyPropertyChanged(nameof(Name)); } }
    public int MaxNameLength { get => _name.MaxLength; }
    public string Description { get => _description.Description; set { _description.Description = value; NotifyPropertyChanged(nameof(Description)); } }
    public int MaxDescriptionLength { get => _description.MaxLength; }

    public static Skill ReadSkill(Stream reader)
    {
        Skill skill = new Skill();
        FixedLengthName? name = reader.ReadStruct<FixedLengthName>();
        if (name == null) return null;
        skill._name = name.Value;

        skill.Stats = SkillStats.ReadSkillStats(reader);

        FixedLengthDescription? description = reader.ReadStruct<FixedLengthDescription>();
        if (description == null) return null;
        skill._description = description.Value;

        return skill;
    }

    public void WriteSkill(Stream writer)
    {
        writer.WriteStruct(_name);
        Stats.WriteSkillStats(writer);
        writer.WriteStruct(_description);
    }
}