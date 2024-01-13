using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.Common.Data.Items;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Starting;

public class CharacterStartingStats : BaseContainer
{
	// used for UI list for specifying who these stats are for
    public string CharacterName { get; private set; }

    public uint Experience { get; set; }
	public short Weapon { get; set; }
	public short Armor { get; set; }
	public short Headgear { get; set; }
	public short Footwear { get; set; }
	public short Accessory { get; set; }
	public short Manaegg { get; set; }
	public short Stamina { get; set; }
	public short Unknown1 { get; set; }
	public short Unknown2 { get; set; }
	public short Unknown3 { get; set; }
	public short Unknown4 { get; set; }
	public short Unknown5 { get; set; }
	public short Unknown6 { get; set; }
	public short Unknown7 { get; set; }
	public short Unknown8 { get; set; }
	public short Unknown9 { get; set; }
	public short Unknown10 { get; set; }
	public short IpStun { get; set; }
	public short IpCancelStun { get; set; }
	public byte ComboSpRegen { get; set; }
	public byte CritSpRegen { get; set; }
	public byte Unknown11 { get; set; }
	public byte HitSpRegen { get; set; }
	public byte Unknown12 { get; set; }
	public byte EvasionStillRate { get; set; }
	public byte EvasionMovingRate { get; set; }
	public byte KnockbackResist { get; set; }
	public short Unknown13 { get; set; }
	public short StatusRecoveryTime { get; set; }
	public short TDMG { get; set; }
	public short Unknown14 { get; set; }
	public short THEAL { get; set; }
	public short Size { get; set; }
	public short Unknown15 { get; set; }
	public short Unknown16 { get; set; }
	public short Unknown17 { get; set; }
	public short Unknown18 { get; set; }
	public short Unknown19 { get; set; }
	public short Unknown20 { get; set; }
	public short Unknown21 { get; set; }
	public short Unknown22 { get; set; }
	public short Unknown23 { get; set; }

	public static string CSVHeader =>
		$"Character,Experience,Weapon,Armor,Headgear,Footwear,Accessory,Manaegg,Stamina,Unknown #1," +
		$"Unknown #2,Unknown #3,Unknown #4,Unknown #5,Unknown #6,Unknown #7,Unknown #8,Unknown #9," +
		$"Unknown #10,IP Stun,IP Cancel Stun,Combo SP Regen,Crit SP Regen,Unknown #11,Hit SP Regen," +
		$"Unknown #12,Still Evasion Rate,Moving Evasion Rate,Knockback Resist,Unknown #13,Status Recovery Time," +
		$"TDMG,Unknown #14,THEAL,Size,Unknown #15,Unknown #16,Unknown #17,Unknown #18,Unknown #19,Unknown #20," +
		$"Unknown #21,Unknown #22,Unknown #23";

	public static CharacterStartingStats ReadCharacterStartingStats(Stream reader, string characterName)
	{
		CharacterStartingStats stats = new()
		{
            CharacterName = characterName,
            Experience = reader.ReadRawUInt(),
			Weapon = reader.ReadRawShort(),
			Armor = reader.ReadRawShort(),
			Headgear = reader.ReadRawShort(),
			Footwear = reader.ReadRawShort(),
			Accessory = reader.ReadRawShort(),
			Manaegg = reader.ReadRawShort(),
			Stamina = reader.ReadRawShort(),
			Unknown1 = reader.ReadRawShort(),
			Unknown2 = reader.ReadRawShort(),
			Unknown3 = reader.ReadRawShort(),
			Unknown4 = reader.ReadRawShort(),
			Unknown5 = reader.ReadRawShort(),
			Unknown6 = reader.ReadRawShort(),
			Unknown7 = reader.ReadRawShort(),
			Unknown8 = reader.ReadRawShort(),
			Unknown9 = reader.ReadRawShort(),
			Unknown10 = reader.ReadRawShort(),
			IpStun = reader.ReadRawShort(),
			IpCancelStun = reader.ReadRawShort(),
			ComboSpRegen = reader.ReadRawByte(),
			CritSpRegen = reader.ReadRawByte(),
			Unknown11 = reader.ReadRawByte(),
			HitSpRegen = reader.ReadRawByte(),
			Unknown12 = reader.ReadRawByte(),
			EvasionStillRate = reader.ReadRawByte(),
			EvasionMovingRate = reader.ReadRawByte(),
			KnockbackResist = reader.ReadRawByte(),
			Unknown13 = reader.ReadRawShort(),
			StatusRecoveryTime = reader.ReadRawShort(),
			TDMG = reader.ReadRawShort(),
			Unknown14 = reader.ReadRawShort(),
			THEAL = reader.ReadRawShort(),
			Size = reader.ReadRawShort(),
			Unknown15 = reader.ReadRawShort(),
			Unknown16 = reader.ReadRawShort(),
			Unknown17 = reader.ReadRawShort(),
			Unknown18 = reader.ReadRawShort(),
			Unknown19 = reader.ReadRawShort(),
			Unknown20 = reader.ReadRawShort(),
			Unknown21 = reader.ReadRawShort(),
			Unknown22 = reader.ReadRawShort(),
			Unknown23 = reader.ReadRawShort(),
		};
		return stats;
	}

	public void WriteCharacterStartingStats(Stream writer)
	{
		writer.WriteRawUInt(Experience);
		writer.WriteRawShort(Weapon);
		writer.WriteRawShort(Armor);
		writer.WriteRawShort(Headgear);
		writer.WriteRawShort(Footwear);
		writer.WriteRawShort(Accessory);
		writer.WriteRawShort(Manaegg);
		writer.WriteRawShort(Stamina);
		writer.WriteRawShort(Unknown1);
		writer.WriteRawShort(Unknown2);
		writer.WriteRawShort(Unknown3);
		writer.WriteRawShort(Unknown4);
		writer.WriteRawShort(Unknown5);
		writer.WriteRawShort(Unknown6);
		writer.WriteRawShort(Unknown7);
		writer.WriteRawShort(Unknown8);
		writer.WriteRawShort(Unknown9);
		writer.WriteRawShort(Unknown10);
		writer.WriteRawShort(IpStun);
		writer.WriteRawShort(IpCancelStun);
		writer.WriteRawByte(ComboSpRegen);
		writer.WriteRawByte(CritSpRegen);
		writer.WriteRawByte(Unknown11);
		writer.WriteRawByte(HitSpRegen);
		writer.WriteRawByte(Unknown12);
		writer.WriteRawByte(EvasionStillRate);
		writer.WriteRawByte(EvasionMovingRate);
		writer.WriteRawByte(KnockbackResist);
		writer.WriteRawShort(Unknown13);
		writer.WriteRawShort(StatusRecoveryTime);
		writer.WriteRawShort(TDMG);
		writer.WriteRawShort(Unknown14);
		writer.WriteRawShort(THEAL);
		writer.WriteRawShort(Size);
		writer.WriteRawShort(Unknown15);
		writer.WriteRawShort(Unknown16);
		writer.WriteRawShort(Unknown17);
		writer.WriteRawShort(Unknown18);
		writer.WriteRawShort(Unknown19);
		writer.WriteRawShort(Unknown20);
		writer.WriteRawShort(Unknown21);
		writer.WriteRawShort(Unknown22);
		writer.WriteRawShort(Unknown23);
	}

	public void GenerateCSV(StreamWriter writer)
	{
		writer.Write(
			$"{CharacterName}," +
			$"{Experience}," +
			$"{Items.Items.Instance.GameItems[Weapon].Name}," +
			$"{Items.Items.Instance.GameItems[Armor].Name}," +
			$"{Items.Items.Instance.GameItems[Headgear].Name}," +
			$"{Items.Items.Instance.GameItems[Footwear].Name}," +
			$"{Items.Items.Instance.GameItems[Accessory].Name}," +
			$"{Items.Items.Instance.GameItems[Manaegg].Name}," +
			$"{Stamina}," +
			$"{Unknown1}," +
			$"{Unknown2}," +
			$"{Unknown3}," +
			$"{Unknown4}," +
			$"{Unknown5}," +
			$"{Unknown6}," +
			$"{Unknown7}," +
			$"{Unknown8}," +
			$"{Unknown9}," +
			$"{Unknown10}," +
			$"{IpStun}," +
			$"{IpCancelStun}," +
			$"{ComboSpRegen}," +
			$"{CritSpRegen}," +
			$"{Unknown11}," +
			$"{HitSpRegen}," +
			$"{Unknown12}," +
			$"{EvasionStillRate}," +
			$"{EvasionMovingRate}," +
			$"{KnockbackResist}," +
			$"{Unknown13}," +
			$"{StatusRecoveryTime}," +
			$"{TDMG}," +
			$"{Unknown14}," +
			$"{THEAL}," +
			$"{Size}," +
			$"{Unknown15}," +
			$"{Unknown16}," +
			$"{Unknown17}," +
			$"{Unknown18}," +
			$"{Unknown19}," +
			$"{Unknown20}," +
			$"{Unknown21}," +
			$"{Unknown22}," +
			$"{Unknown23}");
	}
}
