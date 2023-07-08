using G2DataGUI.Common.Data.Common;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class Skill : BaseContainer
{
    private FixedLengthName _name;
    public SkillStats Stats { get; private set; }
    private FixedLengthDescription _description;

    public string Name
    {
        get => _name.Name;
        set {
            _name.Name = value;
            NotifyPropertyChanged(nameof(Name));
        }
    }
    public int MaxNameLength { get => FixedLengthName.MaxLength; }
    public string Description
    {
        get => _description.Description;
        set {
            _description.Description = value;
            NotifyPropertyChanged(nameof(Description));
        }
    }
    public int MaxDescriptionLength { get => FixedLengthDescription.MaxLength; }

	public static string CSVHeader =>
		$"Name,{SkillStats.CSVHeader},Description";

    public static Skill ReadSkill(Stream reader)
    {
        Skill skill = new()
        {
            _name = FixedLengthName.ReadFixedLengthName(reader),
            Stats = SkillStats.ReadSkillStats(reader),
            _description = FixedLengthDescription.ReadFixedLengthDescription(reader),
        };

        return skill;
    }

    public void WriteSkill(Stream writer)
    {
        _name.WriteFixedLengthName(writer);
        Stats.WriteSkillStats(writer);
        _description.WriteFixedLengthDescription(writer);
    }

	public void GenerateCSV(StreamWriter writer)
	{
		writer.Write($"{Name},");
		Stats.GenerateCSV(writer);
		writer.WriteLine($",{Description}");
	}
}
