using G2DataGUI.Domain.Skills;
using G2DataGUI.Locale;

namespace G2DataGUI.Export.Skills;

public sealed class SkillStatsCsvExporter(ILocalizationService locale)
{
	public string Header =>
		"Cost Type #1,Cost Type #2,Base HP,Base MP,Base SP,Base Strength,Base Vitality,Base Action,Base Movement,Base Magic,Base Mentality," +
		"Unknown #1,Unknown #2,Unknown #3,Unknown #4,Unknown #5,Increase Fire %,Increase Wind %,Increase Earth %,Increase Lightning %," +
		"Increase Blizzard %,Increase Water %,Increase Explosion %,Increase Forest %,Special,Coin Cost Lv1,Coin Cost Lv2,Coin Cost Lv3," +
		"Coin Cost Lv4,Coin Cost Lv5,Multiplier";

	public void WriteFields(TextWriter writer, SkillStats stats)
	{
		writer.Write(
			$"{locale[LocaleKeys.CoinCostTypes][stats.CostType1]}," +
			$"{locale[LocaleKeys.CoinCostTypes][stats.CostType2]}," +
			$"{stats.BaseHP}," +
			$"{stats.BaseMP}," +
			$"{stats.BaseSP}," +
			$"{stats.BaseStrength}," +
			$"{stats.BaseVitality}," +
			$"{stats.BaseAction}," +
			$"{stats.BaseMovement}," +
			$"{stats.BaseMagic}," +
			$"{stats.BaseMentality}," +
			$"{stats.Unknown1}," +
			$"{stats.Unknown2}," +
			$"{stats.Unknown3}," +
			$"{stats.Unknown4}," +
			$"{stats.Unknown5}," +
			$"{stats.IncreaseFirePercent}," +
			$"{stats.IncreaseWindPercent}," +
			$"{stats.IncreaseEarthPercent}," +
			$"{stats.IncreaseLightningPercent}," +
			$"{stats.IncreaseBlizzardPercent}," +
			$"{stats.IncreaseWaterPercent}," +
			$"{stats.IncreaseExplosionPercent}," +
			$"{stats.IncreaseForestPercent}," +
			$"{stats.Special}," +
			$"{stats.CoinCost1}," +
			$"{stats.CoinCost2}," +
			$"{stats.CoinCost3}," +
			$"{stats.CoinCost4}," +
			$"{stats.CoinCost5}," +
			$"{stats.Multiplier}");
	}
}
