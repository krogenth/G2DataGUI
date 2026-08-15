using G2DataGUI.Domain.Items;
using G2DataGUI.Export.Abstractions;
using G2DataGUI.Locale;

namespace G2DataGUI.Export.Items;

/// <summary>
/// New — the original Items.cs never had a GenerateCSV method (ItemsCSVPath
/// existed in ProjectPaths but nothing wrote to it). Written to match the
/// style of the exporters that DO have a precedent, not ported from
/// anything. Equipment/Usable are optional per item, so their columns are
/// blank rather than omitted when absent, keeping the CSV's column count
/// constant across rows.
/// </summary>
public sealed class ItemCsvExporter(ILocalizationService locale) : ICsvExporter<Item>
{
	public string Header =>
		"ID,Name,Description,Entry Type,Unknown #1,Unknown #2,Unknown #3,Icon,Unknown #4,Price," +
		"Has Equipment,Equip: Characters,Equip: Strength,Equip: Vitality,Equip: Action,Equip: Movement," +
		"Equip: Effective On,Equip: Fire Affinity,Equip: Wind Affinity,Equip: Earth Affinity," +
		"Equip: Lightning Affinity,Equip: Blizzard Affinity,Equip: Ailments,Equip: Ailments Chance,Equip: Special," +
		"Has Usable,Usable: Target Effect,Usable: Target Type,Usable: Power,Usable: Range,Usable: Cast Time," +
		"Usable: Recovery Time,Usable: Animation,Usable: Effective On,Usable: Element,Usable: Element Strength," +
		"Usable: Ailments,Usable: Ailments Chance,Usable: Break Chance,Usable: Special";

	public void WriteRow(TextWriter writer, Item item)
	{
		writer.Write(
			$"{item.Id},{item.Name},{item.Description}," +
			$"{item.Stats.EntryType},{item.Stats.Unknown1},{item.Stats.Unknown2},{item.Stats.Unknown3}," +
			$"{locale[LocaleKeys.Icons][item.Stats.Icon]},{item.Stats.Unknown4},{item.Stats.Price},");

		if (item.Equipment is { } equipment)
		{
			writer.Write(
				$"true,{equipment.CharacterBitflag},{equipment.Strength},{equipment.Vitality},{equipment.Action}," +
				$"{equipment.Movement},{equipment.EffectiveOn},{equipment.FireAffinity},{equipment.WindAffinity}," +
				$"{equipment.EarthAffinity},{equipment.LightningAffinity},{equipment.BlizzardAffinity}," +
				$"{equipment.AilmentsBitflag},{equipment.AilmentsChance},{equipment.Special},");
		}
		else
		{
			writer.Write("false,,,,,,,,,,,,,,,");
		}

		if (item.Usable is { } usable)
		{
			writer.Write(
				$"true,{locale[LocaleKeys.TargetEffects][usable.TargetEffect]},{locale[LocaleKeys.TargetTypes][usable.TargetType]}," +
				$"{usable.Power},{usable.Range},{usable.CastTime},{usable.RecoveryTime},{usable.Animation}," +
				$"{usable.EffectiveOn},{locale[LocaleKeys.Elements][usable.Element]},{usable.ElementStrength}," +
				$"{usable.AilmentsBitflag},{usable.AilmentsChance},{usable.BreakChance},{usable.Special}");
		}
		else
		{
			writer.Write("false,,,,,,,,,,,,,");
		}
	}
}
