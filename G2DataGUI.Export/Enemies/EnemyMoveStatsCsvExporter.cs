using G2DataGUI.Domain.Enemies;
using G2DataGUI.Locale;

namespace G2DataGUI.Export.Enemies;

public sealed class EnemyMoveStatsCsvExporter(ILocalizationService locale)
{
	public string Header =>
		"MP,SP,Unknown #1,Target Effect,Strength,Power,AD,Target Type,Normal Attack Flag,Distance," +
		"Accuracy,Range,Cast Time,Recovery Time,Animation,Knockdown,IP Stun,IP Cancel Stun,Knockback," +
		"Element,Element Strength,Ailments,Ailments Chance,Attack Modifier,Defense Modifier,Action Modifier," +
		"Movement Modifier,Special";

	public void WriteFields(TextWriter writer, EnemyMoveStats stats)
	{
		writer.Write(
			$"{stats.MP},{stats.SP},{stats.Unknown1}," +
			$"{locale[LocaleKeys.TargetEffects][stats.TargetEffect]}," +
			$"{stats.Strength},{stats.Power},{stats.AD}," +
			$"{locale[LocaleKeys.TargetTypes][stats.TargetType]}," +
			$"{stats.NormalAttackFlag},{stats.Distance},{stats.Accuracy},{stats.Range},{stats.CastTime},{stats.RecoveryTime}," +
			$"{locale[LocaleKeys.Animations][stats.Animation]}," +
			$"{stats.Knockdown},{stats.IpStun},{stats.IpCancelStun},{stats.Knockback}," +
			$"{locale[LocaleKeys.Elements][stats.Element]}," +
			$"{stats.ElementStrength},{stats.AilmentsBitflag},{stats.AilmentsChance}," +
			$"{stats.AttackModifier},{stats.DefenseModifier},{stats.ActionModifier},{stats.MovementModifier}," +
			$"{stats.Special}");
	}
}
