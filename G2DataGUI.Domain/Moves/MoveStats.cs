using G2DataGUI.Domain.Common;

namespace G2DataGUI.Domain.Moves;

public sealed class MoveStats
{
	public short Cost { get; set; }
	public byte TargetEffect { get; set; }
	public byte TargetType { get; set; }
	public short Strength { get; set; }
	public short Power { get; set; }
	public short Range { get; set; }
	public short Cast1Time { get; set; }
	public short Cast5Time { get; set; }
	public short RecoveryTime { get; set; }
	public short Animation { get; set; }
	public byte Unknown1 { get; set; }
	public bool Knockdown { get; set; }
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
	public short Special { get; set; }
	public short CoinCost1 { get; set; }
	public short CoinCost2 { get; set; }
	public short CoinCost3 { get; set; }
	public short CoinCost4 { get; set; }
	public short CoinCost5 { get; set; }
	public short Multiplier { get; set; }

	public bool PoisonBitflag
	{
		get => HasAilment(AilmentTypes.Poison);
		set => SetAilment(AilmentTypes.Poison, value);
	}
	public bool SleepBitflag
	{
		get => HasAilment(AilmentTypes.Sleep);
		set => SetAilment(AilmentTypes.Sleep, value);
	}
	public bool ParalysisBitflag
	{
		get => HasAilment(AilmentTypes.Paralysis);
		set => SetAilment(AilmentTypes.Paralysis, value);
	}
	public bool ConfusionBitflag
	{
		get => HasAilment(AilmentTypes.Confusion);
		set => SetAilment(AilmentTypes.Confusion, value);
	}
	public bool PlagueBitflag
	{
		get => HasAilment(AilmentTypes.Plague);
		set => SetAilment(AilmentTypes.Plague, value);
	}
	public bool MagicBlockBitflag
	{
		get => HasAilment(AilmentTypes.MagicBlock);
		set => SetAilment(AilmentTypes.MagicBlock, value);
	}
	public bool MoveBlockBitflag
	{
		get => HasAilment(AilmentTypes.MoveBlock);
		set => SetAilment(AilmentTypes.MoveBlock, value);
	}
	public bool DeathBitflag
	{
		get => HasAilment(AilmentTypes.Death);
		set => SetAilment(AilmentTypes.Death, value);
	}

	private bool HasAilment(AilmentTypes flag) =>
		(AilmentsBitflag & (byte)flag) != 0;

	private void SetAilment(AilmentTypes flag, bool value) =>
		AilmentsBitflag = value
			? (byte)(AilmentsBitflag | (byte)flag)
			: (byte)(AilmentsBitflag & ~(byte)flag);
}
