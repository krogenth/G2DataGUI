using G2DataGUI.Domain.Common;
using G2DataGUI.Domain.Items;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.Common.ViewModels.Flags;

namespace G2DataGUI.UI.ViewModels.Items;

/// <summary>
/// AilmentsBitflag stays raw here — see EquipmentViewModel's doc
/// comment on why (method-based flags, not per-flag properties).
/// </summary>
public sealed class UsableViewModel
	: ItemViewModel<Usable>
{
	public AilmentsFlagsSelectorViewModel Ailments { get; }

	public UsableViewModel(Usable model, IRepository repository) : base(model, repository)
	{
		Ailments = new AilmentsFlagsSelectorViewModel("Ailments Inflicted", (AilmentTypes)model.AilmentsBitflag);
		Ailments.FlagsChanged += (_, e) =>
			SetModelValue((byte)e.Value, m => m.AilmentsBitflag, (m, v) => m.AilmentsBitflag = v, nameof(AilmentsBitflag));
	}

	public byte TargetEffect
	{
		get => Model.TargetEffect;
		set => SetModelValue(value, m => m.TargetEffect, (m, v) => m.TargetEffect = v);
	}

	public byte TargetType
	{
		get => Model.TargetType;
		set => SetModelValue(value, m => m.TargetType, (m, v) => m.TargetType = v);
	}

	public short Power
	{
		get => Model.Power;
		set => SetModelValue(value, m => m.Power, (m, v) => m.Power = v);
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

	public ushort Animation
	{
		get => Model.Animation;
		set => SetModelValue(value, m => m.Animation, (m, v) => m.Animation = v);
	}

	public byte EffectiveOn
	{
		get => Model.EffectiveOn;
		set => SetModelValue(value, m => m.EffectiveOn, (m, v) => m.EffectiveOn = v);
	}

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}

	public short IpDamage
	{
		get => Model.IpDamage;
		set => SetModelValue(value, m => m.IpDamage, (m, v) => m.IpDamage = v);
	}

	public short IpCancelDamage
	{
		get => Model.IpCancelDamage;
		set => SetModelValue(value, m => m.IpCancelDamage, (m, v) => m.IpCancelDamage = v);
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

	public byte BreakChance
	{
		get => Model.BreakChance;
		set => SetModelValue(value, m => m.BreakChance, (m, v) => m.BreakChance = v);
	}

	public byte Special
	{
		get => Model.Special;
		set => SetModelValue(value, m => m.Special, (m, v) => m.Special = v);
	}

	public byte Unknown2
	{
		get => Model.Unknown2;
		set => SetModelValue(value, m => m.Unknown2, (m, v) => m.Unknown2 = v);
	}

	public byte Unknown3
	{
		get => Model.Unknown3;
		set => SetModelValue(value, m => m.Unknown3, (m, v) => m.Unknown3 = v);
	}
}
