using G2DataGUI.Domain.Enemies;
using G2DataGUI.Domain.Items;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Items;

namespace G2DataGUI.Export.Enemies;

public sealed class EnemyStatsCsvExporter(ILocalizationService locale, ItemRepository items)
{
	public string Header =>
		"Name,Unknown #1,Unknown #2,Type #1,Type #2,Level,Health,MP,SP,Vitality,Agility,Speed," +
		"Mentality,Stamina,IP Stun Duration,IP Cancel Stun Duration,Still Evasion Rate,Moving Evasion Rate," +
		"Fire Resist,Wind Resist,Earth Resist,Lightning Resist,Blizzard Resist,Ailments," +
		"Knockback Resist,Status Recovery Time,TDMG,Unknown #3,THEAL,Size,Unknown #4,Unknown #5,No Run Flag," +
		"Unknown #6,Experience,Skill Coins,Magic Coins,Gold Coins,Item #1,Item #2,Item #1 Chance,Item #2 Chance," +
		"Unknown #7";

	public void WriteFields(TextWriter writer, EnemyStats stats)
	{
		writer.Write(
			$"{stats.Name}," +
			$"{stats.Unknown1},{stats.Unknown2}," +
			$"{locale[LocaleKeys.EnemyTypes][stats.Type1]},{locale[LocaleKeys.EnemyTypes][stats.Type2]}," +
			$"{stats.Level},{stats.Health},{stats.MP},{stats.SP},{stats.Vitality},{stats.Agility},{stats.Speed}," +
			$"{stats.Mentality},{stats.Stamina},{stats.IpStunDuration},{stats.IpCancelStunDuration}," +
			$"{stats.EvasionStillRate},{stats.EvasionMovingRate}," +
			$"{stats.FireResist},{stats.WindResist},{stats.EarthResist},{stats.LightningResist},{stats.BlizzardResist}," +
			$"{stats.AilmentsBitflag}," +
			$"{stats.KnockbackResist},{stats.StatusRecoveryTime},{stats.TDMG},{stats.Unknown3},{stats.THEAL}," +
			$"{stats.Size},{stats.Unknown4},{stats.Unknown5},{stats.NoRunFlag},{stats.Unknown6}," +
			$"{stats.Experience},{stats.SkillCoins},{stats.MagicCoins},{stats.GoldCoins}," +
			$"{ItemName(stats.Item1Offset)},{ItemName(stats.Item2Offset)},{stats.Item1Chance},{stats.Item2Chance}," +
			$"{stats.Unknown7}");
	}

	private string ItemName(short offset)
	{
		Item? resolved = items.Find(offset);

		return resolved?.Name ?? $"<unknown {offset}>";
	}
}
