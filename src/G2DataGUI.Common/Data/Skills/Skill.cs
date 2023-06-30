using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class Skill : BaseContainer
{
    private FixedLengthName _name;
    private SkillStats _stats;
    private FixedLengthDescription _description;

    public static Skill ReadSkill(Stream reader)
    {
        Skill skill = new Skill();
        FixedLengthName? name = reader.ReadStruct<FixedLengthName>();
        if (name == null) return null;
        skill._name = name.Value;

        SkillStats? stats = reader.ReadStruct<SkillStats>();
        if (stats == null) return null;
        skill._stats = stats.Value;

        FixedLengthDescription? description = reader.ReadStruct<FixedLengthDescription>();
        if (description == null) return null;
        skill._description = description.Value;

        return skill;
    }

    public void WriteSkill(Stream writer)
    {
        writer.WriteStruct(_name);
        writer.WriteStruct(_stats);
        writer.WriteStruct(_description);
    }

    public string Name { get => _name.Name; set { _name.Name = value; NotifyPropertyChanged(nameof(Name)); } }
    public int MaxNameLength { get => _name.MaxLength; }
    public byte Cost1 { get => _stats.Cost1; set => _stats.Cost1 = value; }
    public byte Cost2 { get => _stats.Cost2; set => _stats.Cost2 = value; }
    public short BaseHP { get => _stats.BaseHP; set => _stats.BaseHP = value; }
    public short BaseMP { get => _stats.BaseMP; set => _stats.BaseMP = value; }
    public short BaseSP { get => _stats.BaseSP; set => _stats.BaseSP = value; }
    public short BaseStrength { get => _stats.BaseStrength; set => _stats.BaseStrength = value; }
    public short BaseVitality { get => _stats.BaseVitality; set => _stats.BaseVitality = value; }
    public short BaseAction { get => _stats.BaseAction; set => _stats.BaseAction = value; }
    public short BaseMovement { get => _stats.BaseMovement; set => _stats.BaseMovement = value; }
    public short BaseMagic { get => _stats.BaseMagic; set => _stats.BaseMagic = value; }
    public short BaseMentality { get => _stats.BaseMentality; set => _stats.BaseMentality = value; }
    public byte Unknown1 { get => _stats.Unknown1; set => _stats.Unknown1 = value; }
    public byte Unknown2 { get => _stats.Unknown2; set => _stats.Unknown2 = value; }
    public byte Unknown3 { get => _stats.Unknown3; set => _stats.Unknown3 = value; }
    public byte Unknown4 { get => _stats.Unknown4; set => _stats.Unknown4 = value; }
    public byte Unknown5 { get => _stats.Unknown5; set => _stats.Unknown5 = value; }
    public sbyte IncreaseFirePercent { get => _stats.IncreaseFirePercent; set => _stats.IncreaseFirePercent = value; }
    public sbyte IncreaseWindPercent { get => _stats.IncreaseWindPercent; set => _stats.IncreaseWindPercent = value; }
    public sbyte IncreaseEarthPercent { get => _stats.IncreaseEarthPercent; set => _stats.IncreaseEarthPercent = value; }
    public sbyte IncreaseLightningPercent { get => _stats.IncreaseLightningPercent; set => _stats.IncreaseLightningPercent = value; }
    public sbyte IncreaseBlizzardPercent { get => _stats.IncreaseBlizzardPercent; set => _stats.IncreaseBlizzardPercent = value; }
    public sbyte IncreaseWaterPercent { get => _stats.IncreaseWaterPercent; set => _stats.IncreaseWaterPercent = value; }
    public sbyte IncreaseExplosionPercent { get => _stats.IncreaseExplosionPercent; set => _stats.IncreaseExplosionPercent = value; }
    public sbyte IncreaseForestPercent { get => _stats.IncreaseForestPercent; set => _stats.IncreaseForestPercent = value; }
    public byte Special { get => _stats.Special; set => _stats.Special = value; }
    public short CoinCost1 { get => _stats.CoinCost1; set => _stats.CoinCost1 = value; }
    public short CoinCost2 { get => _stats.CoinCost2; set => _stats.CoinCost2 = value; }
    public short CoinCost3 { get => _stats.CoinCost3; set => _stats.CoinCost3 = value; }
    public short CoinCost4 { get => _stats.CoinCost4; set => _stats.CoinCost4 = value; }
    public short CoinCost5 { get => _stats.CoinCost5; set => _stats.CoinCost5 = value; }
    public short Multiplier { get => _stats.Multiplier; set => _stats.Multiplier = value; }
    public string Description { get => _description.Description; set {  _description.Description = value; NotifyPropertyChanged(nameof(Description)); } }
    public int MaxDescriptionLength { get => _description.MaxLength; }
}