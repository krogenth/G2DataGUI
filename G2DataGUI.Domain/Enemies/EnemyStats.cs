namespace G2DataGUI.Domain.Enemies;

/// <summary>
/// Shared by both Enemy and Boss — identical binary layout for
/// both (same pointer offsets, same field order). See EnemyEntrySerializer.
/// </summary>
public sealed class EnemyStats
{
	public string Name { get; set; } = string.Empty;
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Type1 { get; set; }
	public byte Type2 { get; set; }
	public short Level { get; set; }
	public int Health { get; set; }
	public short MP { get; set; }
	public short SP { get; set; }
	public short Vitality { get; set; }
	public short Agility { get; set; }
	public short Speed { get; set; }
	public short Mentality { get; set; }
	public short Stamina { get; set; }
	public short IpStunDuration { get; set; }
	public short IpCancelStunDuration { get; set; }
	public byte EvasionStillRate { get; set; }
	public byte EvasionMovingRate { get; set; }
	public sbyte FireResist { get; set; }
	public sbyte WindResist { get; set; }
	public sbyte EarthResist { get; set; }
	public sbyte LightningResist { get; set; }
	public sbyte BlizzardResist { get; set; }
	public byte AilmentsBitflag { get; set; }
	public short KnockbackResist { get; set; }
	public short StatusRecoveryTime { get; set; }
	public short TDMG { get; set; }
	public short Unknown3 { get; set; }
	public short THEAL { get; set; }
	public short Size { get; set; }
	public short Unknown4 { get; set; }
	public byte Unknown5 { get; set; }
	public bool NoRunFlag { get; set; }
	public short Unknown6 { get; set; }
	public int Experience { get; set; }
	public int SkillCoins { get; set; }
	public int MagicCoins { get; set; }
	public int GoldCoins { get; set; }

	/// <summary>
	/// Index into Items — resolved via ItemRepository.Find in the exporter.
	/// </summary>
	public short Item1Offset { get; set; }
	public short Item2Offset { get; set; }
	public byte Item1Chance { get; set; }
	public byte Item2Chance { get; set; }
	public short Unknown7 { get; set; }
}
