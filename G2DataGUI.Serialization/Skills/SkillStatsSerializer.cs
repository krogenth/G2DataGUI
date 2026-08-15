using G2DataGUI.Domain.Skills;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Skills;

public static class SkillStatsSerializer
{
	public static SkillStats Read(Stream stream) => new()
	{
		CostType1 = stream.ReadRawByte(),
		CostType2 = stream.ReadRawByte(),
		BaseHP = stream.ReadRawShort(),
		BaseMP = stream.ReadRawShort(),
		BaseSP = stream.ReadRawShort(),
		BaseStrength = stream.ReadRawShort(),
		BaseVitality = stream.ReadRawShort(),
		BaseAction = stream.ReadRawShort(),
		BaseMovement = stream.ReadRawShort(),
		BaseMagic = stream.ReadRawShort(),
		BaseMentality = stream.ReadRawShort(),
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		Unknown3 = stream.ReadRawByte(),
		Unknown4 = stream.ReadRawByte(),
		Unknown5 = stream.ReadRawByte(),
		IncreaseFirePercent = stream.ReadRawSByte(),
		IncreaseWindPercent = stream.ReadRawSByte(),
		IncreaseEarthPercent = stream.ReadRawSByte(),
		IncreaseLightningPercent = stream.ReadRawSByte(),
		IncreaseBlizzardPercent = stream.ReadRawSByte(),
		IncreaseWaterPercent = stream.ReadRawSByte(),
		IncreaseExplosionPercent = stream.ReadRawSByte(),
		IncreaseForestPercent = stream.ReadRawSByte(),
		Special = stream.ReadRawByte(),
		CoinCost1 = stream.ReadRawShort(),
		CoinCost2 = stream.ReadRawShort(),
		CoinCost3 = stream.ReadRawShort(),
		CoinCost4 = stream.ReadRawShort(),
		CoinCost5 = stream.ReadRawShort(),
		Multiplier = stream.ReadRawShort(),
	};

	public static void Write(Stream stream, SkillStats stats)
	{
		stream.WriteRawByte(stats.CostType1);
		stream.WriteRawByte(stats.CostType2);
		stream.WriteRawShort(stats.BaseHP);
		stream.WriteRawShort(stats.BaseMP);
		stream.WriteRawShort(stats.BaseSP);
		stream.WriteRawShort(stats.BaseStrength);
		stream.WriteRawShort(stats.BaseVitality);
		stream.WriteRawShort(stats.BaseAction);
		stream.WriteRawShort(stats.BaseMovement);
		stream.WriteRawShort(stats.BaseMagic);
		stream.WriteRawShort(stats.BaseMentality);
		stream.WriteRawByte(stats.Unknown1);
		stream.WriteRawByte(stats.Unknown2);
		stream.WriteRawByte(stats.Unknown3);
		stream.WriteRawByte(stats.Unknown4);
		stream.WriteRawByte(stats.Unknown5);
		stream.WriteRawSByte(stats.IncreaseFirePercent);
		stream.WriteRawSByte(stats.IncreaseWindPercent);
		stream.WriteRawSByte(stats.IncreaseEarthPercent);
		stream.WriteRawSByte(stats.IncreaseLightningPercent);
		stream.WriteRawSByte(stats.IncreaseBlizzardPercent);
		stream.WriteRawSByte(stats.IncreaseWaterPercent);
		stream.WriteRawSByte(stats.IncreaseExplosionPercent);
		stream.WriteRawSByte(stats.IncreaseForestPercent);
		stream.WriteRawByte(stats.Special);
		stream.WriteRawShort(stats.CoinCost1);
		stream.WriteRawShort(stats.CoinCost2);
		stream.WriteRawShort(stats.CoinCost3);
		stream.WriteRawShort(stats.CoinCost4);
		stream.WriteRawShort(stats.CoinCost5);
		stream.WriteRawShort(stats.Multiplier);
	}
}
