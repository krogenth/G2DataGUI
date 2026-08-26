using G2DataGUI.Domain.Enemies;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Enemies;

public sealed class EnemyMoveStatsViewModel(EnemyMoveStats model, IRepository repository)
	: ItemViewModel<EnemyMoveStats>(model, repository)
{
	public short MP
	{
		get => Model.MP;
		set => SetModelValue(value, m => m.MP, (m, v) => m.MP = v);
	}

	public short SP
	{
		get => Model.SP;
		set => SetModelValue(value, m => m.SP, (m, v) => m.SP = v);
	}

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}

	public byte TargetEffect
	{
		get => Model.TargetEffect;
		set => SetModelValue(value, m => m.TargetEffect, (m, v) => m.TargetEffect = v);
	}

	public short Strength
	{
		get => Model.Strength;
		set => SetModelValue(value, m => m.Strength, (m, v) => m.Strength = v);
	}

	public short Power
	{
		get => Model.Power;
		set => SetModelValue(value, m => m.Power, (m, v) => m.Power = v);
	}

	public short AD
	{
		get => Model.AD;
		set => SetModelValue(value, m => m.AD, (m, v) => m.AD = v);
	}

	public byte TargetType
	{
		get => Model.TargetType;
		set => SetModelValue(value, m => m.TargetType, (m, v) => m.TargetType = v);
	}

	public bool NormalAttackFlag
	{
		get => Model.NormalAttackFlag;
		set => SetModelValue(value, m => m.NormalAttackFlag, (m, v) => m.NormalAttackFlag = v);
	}

	public short Distance
	{
		get => Model.Distance;
		set => SetModelValue(value, m => m.Distance, (m, v) => m.Distance = v);
	}

	public short Accuracy
	{
		get => Model.Accuracy;
		set => SetModelValue(value, m => m.Accuracy, (m, v) => m.Accuracy = v);
	}

	public short Range
	{
		get => Model.Range;
		set => SetModelValue(value, m => m.Range, (m, v) => m.Range = v);
	}

	public short CastTime
	{
		get => Model.CastTime;
		set => SetModelValue(value, m => m.CastTime, (m, v) => m.CastTime = v);
	}

	public short RecoveryTime
	{
		get => Model.RecoveryTime;
		set => SetModelValue(value, m => m.RecoveryTime, (m, v) => m.RecoveryTime = v);
	}

	public byte Animation
	{
		get => Model.Animation;
		set => SetModelValue(value, m => m.Animation, (m, v) => m.Animation = v);
	}

	public bool Knockdown
	{
		get => Model.Knockdown;
		set => SetModelValue(value, m => m.Knockdown, (m, v) => m.Knockdown = v);
	}

	public short IpStun
	{
		get => Model.IpStun;
		set => SetModelValue(value, m => m.IpStun, (m, v) => m.IpStun = v);
	}

	public short IpCancelStun
	{
		get => Model.IpCancelStun;
		set => SetModelValue(value, m => m.IpCancelStun, (m, v) => m.IpCancelStun = v);
	}

	public short Knockback
	{
		get => Model.Knockback;
		set => SetModelValue(value, m => m.Knockback, (m, v) => m.Knockback = v);
	}

	public byte Element
	{
		get => Model.Element;
		set => SetModelValue(value, m => m.Element, (m, v) => m.Element = v);
	}

	public byte ElementStrength
	{
		get => Model.ElementStrength;
		set => SetModelValue(value, m => m.ElementStrength, (m, v) => m.ElementStrength = v);
	}

	public byte AilmentsBitflag
	{
		get => Model.AilmentsBitflag;
		set => SetModelValue(value, m => m.AilmentsBitflag, (m, v) => m.AilmentsBitflag = v);
	}

	public byte AilmentsChance
	{
		get => Model.AilmentsChance;
		set => SetModelValue(value, m => m.AilmentsChance, (m, v) => m.AilmentsChance = v);
	}

	public sbyte AttackModifier
	{
		get => Model.AttackModifier;
		set => SetModelValue(value, m => m.AttackModifier, (m, v) => m.AttackModifier = v);
	}

	public sbyte DefenseModifier
	{
		get => Model.DefenseModifier;
		set => SetModelValue(value, m => m.DefenseModifier, (m, v) => m.DefenseModifier = v);
	}

	public sbyte ActionModifier
	{
		get => Model.ActionModifier;
		set => SetModelValue(value, m => m.ActionModifier, (m, v) => m.ActionModifier = v);
	}

	public sbyte MovementModifier
	{
		get => Model.MovementModifier;
		set => SetModelValue(value, m => m.MovementModifier, (m, v) => m.MovementModifier = v);
	}

	public short Special
	{
		get => Model.Special;
		set => SetModelValue(value, m => m.Special, (m, v) => m.Special = v);
	}
}
