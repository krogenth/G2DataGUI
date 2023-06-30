using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class SkillStats
{
    public byte CostType1 { get; set; }
    public byte CostType2 { get; set; }
    public short BaseHP { get; set; }
    public short BaseMP { get; set; }
    public short BaseSP { get; set; }
    public short BaseStrength { get; set; }
    public short BaseVitality { get; set; }
    public short BaseAction { get; set; }
    public short BaseMovement { get; set; }
    public short BaseMagic { get; set; }
    public short BaseMentality { get; set; }
    public byte Unknown1 { get; set; }
    public byte Unknown2 { get; set; }
    public byte Unknown3 { get; set; }
    public byte Unknown4 { get; set; }
    public byte Unknown5 { get; set; }
    public sbyte IncreaseFirePercent { get; set; }
    public sbyte IncreaseWindPercent { get; set; }
    public sbyte IncreaseEarthPercent { get; set; }
    public sbyte IncreaseLightningPercent { get; set; }
    public sbyte IncreaseBlizzardPercent { get; set; }
    public sbyte IncreaseWaterPercent { get; set; }
    public sbyte IncreaseExplosionPercent { get; set; }
    public sbyte IncreaseForestPercent { get; set; }
    public byte Special { get; set; }
    public short CoinCost1 { get; set; }
    public short CoinCost2 { get; set; }
    public short CoinCost3 { get; set; }
    public short CoinCost4 { get; set; }
    public short CoinCost5 { get; set; }
    public short Multiplier { get; set; }

    public static SkillStats ReadSkillStats(Stream reader)
    {
        SkillStats stats = new SkillStats();
        stats.CostType1 = reader.ReadRawByte();
        stats.CostType2 = reader.ReadRawByte();
        stats.BaseHP = reader.ReadRawShort();
        stats.BaseMP = reader.ReadRawShort();
        stats.BaseSP = reader.ReadRawShort();
        stats.BaseStrength = reader.ReadRawShort();
        stats.BaseVitality = reader.ReadRawShort();
        stats.BaseAction = reader.ReadRawShort();
        stats.BaseMovement = reader.ReadRawShort();
        stats.BaseMagic = reader.ReadRawShort();
        stats.BaseMentality = reader.ReadRawShort();
        stats.Unknown1 = reader.ReadRawByte();
        stats.Unknown2 = reader.ReadRawByte();
        stats.Unknown3 = reader.ReadRawByte();
        stats.Unknown4 = reader.ReadRawByte();
        stats.Unknown5 = reader.ReadRawByte();
        stats.IncreaseFirePercent = reader.ReadRawSByte();
        stats.IncreaseWindPercent = reader.ReadRawSByte();
        stats.IncreaseEarthPercent = reader.ReadRawSByte();
        stats.IncreaseLightningPercent = reader.ReadRawSByte();
        stats.IncreaseBlizzardPercent = reader.ReadRawSByte();
        stats.IncreaseWaterPercent = reader.ReadRawSByte();
        stats.IncreaseExplosionPercent = reader.ReadRawSByte();
        stats.IncreaseForestPercent = reader.ReadRawSByte();
        stats.Special = reader.ReadRawByte();
        stats.CoinCost1 = reader.ReadRawShort();
        stats.CoinCost2 = reader.ReadRawShort();
        stats.CoinCost3 = reader.ReadRawShort();
        stats.CoinCost4 = reader.ReadRawShort();
        stats.CoinCost5 = reader.ReadRawShort();
        stats.Multiplier = reader.ReadRawShort();

        return stats;
    }

    public void WriteSkillStats(Stream writer)
    {
        writer.WriteRawByte(CostType1);
        writer.WriteRawByte(CostType2);
        writer.WriteRawShort(BaseHP);
        writer.WriteRawShort(BaseMP);
        writer.WriteRawShort(BaseSP);
        writer.WriteRawShort(BaseStrength);
        writer.WriteRawShort(BaseVitality);
        writer.WriteRawShort(BaseAction);
        writer.WriteRawShort(BaseMovement);
        writer.WriteRawShort(BaseMagic);
        writer.WriteRawShort(BaseMentality);
        writer.WriteRawByte(Unknown1);
        writer.WriteRawByte(Unknown2);
        writer.WriteRawByte(Unknown3);
        writer.WriteRawByte(Unknown4);
        writer.WriteRawByte(Unknown5);
        writer.WriteRawSByte(IncreaseFirePercent);
        writer.WriteRawSByte(IncreaseWindPercent);
        writer.WriteRawSByte(IncreaseEarthPercent);
        writer.WriteRawSByte(IncreaseLightningPercent);
        writer.WriteRawSByte(IncreaseBlizzardPercent);
        writer.WriteRawSByte(IncreaseWaterPercent);
        writer.WriteRawSByte(IncreaseExplosionPercent);
        writer.WriteRawSByte(IncreaseForestPercent);
        writer.WriteRawByte(Special);
        writer.WriteRawShort(CoinCost1);
        writer.WriteRawShort(CoinCost2);
        writer.WriteRawShort(CoinCost3);
        writer.WriteRawShort(CoinCost4);
        writer.WriteRawShort(CoinCost5);
        writer.WriteRawShort(Multiplier);
    }
}