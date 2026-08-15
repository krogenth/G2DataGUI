using G2DataGUI.Domain.Items;
using G2DataGUI.Domain.Starting;
using G2DataGUI.Export.Abstractions;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Items;

namespace G2DataGUI.Export.Starting;

public sealed class CharacterStartingStatsCsvExporter(ILocalizationService locale, ItemRepository items) : ICsvExporter<CharacterStartingStats>
{
	public string Header =>
		"Character,Experience,Weapon,Armor,Headgear,Footwear,Accessory,Manaegg,Stamina,Unknown #1," +
		"Unknown #2,Unknown #3,Unknown #4,Unknown #5,Unknown #6,Unknown #7,Unknown #8,Unknown #9," +
		"Unknown #10,IP Stun,IP Cancel Stun,Combo SP Regen,Crit SP Regen,Unknown #11,Hit SP Regen," +
		"Unknown #12,Still Evasion Rate,Moving Evasion Rate,Knockback Resist,Unknown #13,Status Recovery Time," +
		"TDMG,Unknown #14,THEAL,Size,Unknown #15,Unknown #16,Unknown #17,Unknown #18,Unknown #19,Unknown #20," +
		"Unknown #21,Unknown #22,Unknown #23";

	public void WriteRow(TextWriter writer, CharacterStartingStats item)
	{
		writer.Write(
			$"{locale[LocaleKeys.Characters][item.Id]}," +
			$"{item.Experience}," +
			$"{ItemName(item.Weapon)}," +
			$"{ItemName(item.Armor)}," +
			$"{ItemName(item.Headgear)}," +
			$"{ItemName(item.Footwear)}," +
			$"{ItemName(item.Accessory)}," +
			$"{ItemName(item.Manaegg)}," +
			$"{item.Stamina},{item.Unknown1},{item.Unknown2},{item.Unknown3},{item.Unknown4},{item.Unknown5}," +
			$"{item.Unknown6},{item.Unknown7},{item.Unknown8},{item.Unknown9},{item.Unknown10}," +
			$"{item.IpStun},{item.IpCancelStun},{item.ComboSpRegen},{item.CritSpRegen},{item.Unknown11}," +
			$"{item.HitSpRegen},{item.Unknown12},{item.EvasionStillRate},{item.EvasionMovingRate}," +
			$"{item.KnockbackResist},{item.Unknown13},{item.StatusRecoveryTime},{item.TDMG},{item.Unknown14}," +
			$"{item.THEAL},{item.Size},{item.Unknown15},{item.Unknown16},{item.Unknown17},{item.Unknown18}," +
			$"{item.Unknown19},{item.Unknown20},{item.Unknown21},{item.Unknown22},{item.Unknown23}");
	}

	private string ItemName(short offset)
	{
		Item? resolved = items.Find(offset);

		return resolved?.Name ?? $"<unknown {offset}>";
	}
}
