namespace G2DataGUI.Domain.Enemies;

public sealed class EnemyMoveStats
{
	public short MP { get; set; }
	public short SP { get; set; }
	public byte Unknown1 { get; set; }
	public byte TargetEffect { get; set; }
	public short Strength { get; set; }
	public short Power { get; set; }
	public short AD { get; set; }
	public byte TargetType { get; set; }
	public bool NormalAttackFlag { get; set; }
	public short Distance { get; set; }
	public short Accuracy { get; set; }
	public short Range { get; set; }
	public short CastTime { get; set; }
	public short RecoveryTime { get; set; }
	public byte Animation { get; set; }
	public bool Knockdown { get; set; }
	public short IpStun { get; set; }
	public short IpCancelStun { get; set; }
	public short Knockback { get; set; }
	public byte Element { get; set; }
	public byte ElementStrength { get; set; }
	public byte AilmentsBitflag { get; set; }
	public byte AilmentsChance { get; set; }
	public sbyte AttackModifier { get; set; }
	public sbyte DefenseModifier { get; set; }
	public sbyte ActionModifier { get; set; }
	public sbyte MovementModifier { get; set; }
	public short Special { get; set; }
}
