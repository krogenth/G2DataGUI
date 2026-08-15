using G2DataGUI.Domain.Common;

namespace G2DataGUI.Domain.Items;

/// <summary>
/// Present on an Item only when its usable-block pointer was
/// valid — see Equipment for the same convention.
/// </summary>
public sealed class Usable
{
	public const uint ByteSize = 0x20;

	public byte TargetEffect { get; set; }
	public byte TargetType { get; set; }
	public short Power { get; set; }
	public short Range { get; set; }
	public short CastTime { get; set; }
	public short RecoveryTime { get; set; }
	public ushort Animation { get; set; }
	public byte EffectiveOn { get; set; }
	public byte Unknown1 { get; set; }
	public short IpDamage { get; set; }
	public short IpCancelDamage { get; set; }
	public short Knockback { get; set; }
	public byte Element { get; set; }
	public byte ElementStrength { get; set; }
	public byte AilmentsBitflag { get; set; }
	public byte AilmentsChance { get; set; }
	public sbyte AttackModifier { get; set; }
	public sbyte DefenseModifier { get; set; }
	public sbyte ActionModifier { get; set; }
	public sbyte MovementModifier { get; set; }
	public byte BreakChance { get; set; }
	public byte Special { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }

	public bool AilmentFlag(AilmentTypes ailment) =>
		(AilmentsBitflag & (byte)ailment) != 0;

	public void SetAilmentFlag(AilmentTypes ailment, bool value) =>
		AilmentsBitflag = value
			? (byte)(AilmentsBitflag | (byte)ailment)
			: (byte)(AilmentsBitflag & ~(byte)ailment);
}
