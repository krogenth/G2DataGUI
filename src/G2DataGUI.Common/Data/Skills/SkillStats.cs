using System.IO;

using G2DataGUI.IO.Streams;
using G2DataGUI.UI.Common.Locale;

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

	public static string CSVHeader =>
		$"Cost Type #1,Cost Type #2,Base HP,Base MP,Base SP,Base Strength,Base Vitality,Base Action,Base Movement,Base Magic,Base Mentality," +
		$"Unknown #1,Unknown #2,Unknown #3,Unknown #4,Unknown #5,Increase Fire %,Increase Wind %,Increase Earth %,Increase Lightning %," +
		$"Increase Blizzard %,Increase Water %,Increase Explosion %,Increase Forest %,Special,Coin Cost Lv1,Coin Cost Lv2,Coin Cost Lv3," +
		$"Coin Cost Lv4,Coin Cost Lv5,Multiplier";

	public static SkillStats ReadSkillStats(Stream reader)
	{
		SkillStats stats = new()
		{
			CostType1 = reader.ReadRawByte(),
			CostType2 = reader.ReadRawByte(),
			BaseHP = reader.ReadRawShort(),
			BaseMP = reader.ReadRawShort(),
			BaseSP = reader.ReadRawShort(),
			BaseStrength = reader.ReadRawShort(),
			BaseVitality = reader.ReadRawShort(),
			BaseAction = reader.ReadRawShort(),
			BaseMovement = reader.ReadRawShort(),
			BaseMagic = reader.ReadRawShort(),
			BaseMentality = reader.ReadRawShort(),
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Unknown3 = reader.ReadRawByte(),
			Unknown4 = reader.ReadRawByte(),
			Unknown5 = reader.ReadRawByte(),
			IncreaseFirePercent = reader.ReadRawSByte(),
			IncreaseWindPercent = reader.ReadRawSByte(),
			IncreaseEarthPercent = reader.ReadRawSByte(),
			IncreaseLightningPercent = reader.ReadRawSByte(),
			IncreaseBlizzardPercent = reader.ReadRawSByte(),
			IncreaseWaterPercent = reader.ReadRawSByte(),
			IncreaseExplosionPercent = reader.ReadRawSByte(),
			IncreaseForestPercent = reader.ReadRawSByte(),
			Special = reader.ReadRawByte(),
			CoinCost1 = reader.ReadRawShort(),
			CoinCost2 = reader.ReadRawShort(),
			CoinCost3 = reader.ReadRawShort(),
			CoinCost4 = reader.ReadRawShort(),
			CoinCost5 = reader.ReadRawShort(),
			Multiplier = reader.ReadRawShort()
		};

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

	public void GenerateCSV(StreamWriter writer)
	{
		writer.Write(
			$"{LocaleManager.Instance[LocaleKeys.CoinCostTypes][CostType1]}," +
			$"{LocaleManager.Instance[LocaleKeys.CoinCostTypes][CostType2]}," +
			$"{BaseHP}," +
			$"{BaseMP}," +
			$"{BaseSP}," +
			$"{BaseStrength}," +
			$"{BaseVitality}," +
			$"{BaseAction}," +
			$"{BaseMovement}," +
			$"{BaseMagic}," +
			$"{BaseMentality}," +
			$"{Unknown1}," +
			$"{Unknown2}," +
			$"{Unknown3}," +
			$"{Unknown4}," +
			$"{Unknown5}," +
			$"{IncreaseFirePercent}," +
			$"{IncreaseWindPercent}," +
			$"{IncreaseEarthPercent}," +
			$"{IncreaseLightningPercent}," +
			$"{IncreaseBlizzardPercent}," +
			$"{IncreaseWaterPercent}," +
			$"{IncreaseExplosionPercent}," +
			$"{IncreaseForestPercent}," +
			$"{Special}," +
			$"{CoinCost1}," +
			$"{CoinCost2}," +
			$"{CoinCost3}," +
			$"{CoinCost4}," +
			$"{CoinCost5}," +
			$"{Multiplier}");
	}
}
