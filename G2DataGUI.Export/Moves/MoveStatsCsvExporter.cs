using G2DataGUI.Domain.Moves;
using G2DataGUI.Locale;

namespace G2DataGUI.Export.Moves;

public sealed class MoveStatsCsvExporter(ILocalizationService locale)
{
	public string Header =>
		"Cost,Target Effect,Target Type,Strength,Power,Range,Cast 1 Time,Cast 5 Time,Recovery Time," +
		"Animation,Unknown #1,Knockdown,IP Damage,IP Cancel Damage,Knockback,Element,Element Strength," +
		"Ailments,Ailments Chance,Attack Modifier,Defense Modifier,Action Modifier,Move Modifier," +
		"Special,Coin Cost Lv1,Coin Cost Lv2,Coin Cost Lv3,Coin Cost Lv4,Coin Cost Lv5,Multiplier";

	public void WriteFields(TextWriter writer, MoveStats stats)
	{
		writer.Write(
			$"{stats.Cost}," +
			$"{locale[LocaleKeys.TargetEffects][stats.TargetEffect]}," +
			$"{locale[LocaleKeys.TargetTypes][stats.TargetType]}," +
			$"{stats.Strength}," +
			$"{stats.Power}," +
			$"{stats.Range}," +
			$"{stats.Cast1Time}," +
			$"{stats.Cast5Time}," +
			$"{stats.RecoveryTime}," +
			$"{locale[LocaleKeys.Animations][stats.Animation]}," +
			$"{stats.Unknown1}," +
			$"{stats.Knockdown}," +
			$"{stats.IpDamage}," +
			$"{stats.IpCancelDamage}," +
			$"{stats.Knockback}," +
			$"{locale[LocaleKeys.Elements][stats.Element]}," +
			$"{stats.ElementStrength}," +
			$"{stats.AilmentsBitflag}," +
			$"{stats.AilmentsChance}," +
			$"{stats.AttackModifier}," +
			$"{stats.DefenseModifier}," +
			$"{stats.ActionModifier}," +
			$"{stats.MovementModifier}," +
			$"{stats.Special}," +
			$"{stats.CoinCost1}," +
			$"{stats.CoinCost2}," +
			$"{stats.CoinCost3}," +
			$"{stats.CoinCost4}," +
			$"{stats.CoinCost5}," +
			$"{stats.Multiplier}");
	}
}
