using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Domain.Starting;

public sealed class CharacterStartingStats : IIdentifiable<int>
{
	/// <summary>Position within PC_INIT.BIN — resolves to a display name via
	/// LocaleKeys.Characters, same convention as CharacterLevelups.Id.</summary>
	public int Id { get; set; }

	public uint Experience { get; set; }

	/// <summary>Index into Items — resolved via ItemRepository.Find in the exporter.</summary>
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
}
