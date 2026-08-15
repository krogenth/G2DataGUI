using G2DataGUI.Domain.Moves;
using G2DataGUI.Locale;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Moves;

/// <summary>
/// Wraps Move.Stats. Takes the same IRepository as the owning MoveViewModel
/// (Stats isn't itself repository-owned, but an edit to it still needs to
/// mark the containing Move's repository dirty) — see MoveViewModel.
/// </summary>
public sealed class MoveStatsViewModel(MoveStats model, IRepository repository, ILocalizationService locale)
	: ItemViewModel<MoveStats>(model, repository)
{
	public string TargetEffectName => locale[LocaleKeys.TargetEffects][Model.TargetEffect];
	public string TargetTypeName => locale[LocaleKeys.TargetTypes][Model.TargetType];
	public string AnimationName => locale[LocaleKeys.Animations][Model.Animation];
	public string ElementName => locale[LocaleKeys.Elements][Model.Element];

	public short Cost
	{
		get => Model.Cost;
		set => SetModelValue(value, m => m.Cost, (m, v) => m.Cost = v);
	}

	public byte TargetEffect
	{
		get => Model.TargetEffect;
		set
		{
			SetModelValue(value, m => m.TargetEffect, (m, v) => m.TargetEffect = v);
			OnPropertyChanged(nameof(TargetEffectName));
		}
	}

	public byte TargetType
	{
		get => Model.TargetType;
		set
		{
			SetModelValue(value, m => m.TargetType, (m, v) => m.TargetType = v);
			OnPropertyChanged(nameof(TargetTypeName));
		}
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

	public short Range
	{
		get => Model.Range;
		set => SetModelValue(value, m => m.Range, (m, v) => m.Range = v);
	}

	public short Cast1Time
	{
		get => Model.Cast1Time;
		set => SetModelValue(value, m => m.Cast1Time, (m, v) => m.Cast1Time = v);
	}

	public short Cast5Time
	{
		get => Model.Cast5Time;
		set => SetModelValue(value, m => m.Cast5Time, (m, v) => m.Cast5Time = v);
	}

	public short RecoveryTime
	{
		get => Model.RecoveryTime;
		set => SetModelValue(value, m => m.RecoveryTime, (m, v) => m.RecoveryTime = v);
	}

	public short Animation
	{
		get => Model.Animation;
		set
		{
			SetModelValue(value, m => m.Animation, (m, v) => m.Animation = v);
			OnPropertyChanged(nameof(AnimationName));
		}
	}

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}

	public bool Knockdown
	{
		get => Model.Knockdown;
		set => SetModelValue(value, m => m.Knockdown, (m, v) => m.Knockdown = v);
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
		set
		{
			SetModelValue(value, m => m.Element, (m, v) => m.Element = v);
			OnPropertyChanged(nameof(ElementName));
		}
	}

	public byte ElementStrength
	{
		get => Model.ElementStrength;
		set => SetModelValue(value, m => m.ElementStrength, (m, v) => m.ElementStrength = v);
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

	public short CoinCost1
	{
		get => Model.CoinCost1;
		set => SetModelValue(value, m => m.CoinCost1, (m, v) => m.CoinCost1 = v);
	}

	public short CoinCost2
	{
		get => Model.CoinCost2;
		set => SetModelValue(value, m => m.CoinCost2, (m, v) => m.CoinCost2 = v);
	}

	public short CoinCost3
	{
		get => Model.CoinCost3;
		set => SetModelValue(value, m => m.CoinCost3, (m, v) => m.CoinCost3 = v);
	}

	public short CoinCost4
	{
		get => Model.CoinCost4;
		set => SetModelValue(value, m => m.CoinCost4, (m, v) => m.CoinCost4 = v);
	}

	public short CoinCost5
	{
		get => Model.CoinCost5;
		set => SetModelValue(value, m => m.CoinCost5, (m, v) => m.CoinCost5 = v);
	}

	public short Multiplier
	{
		get => Model.Multiplier;
		set => SetModelValue(value, m => m.Multiplier, (m, v) => m.Multiplier = v);
	}

	public bool PoisonBitflag
	{
		get => Model.PoisonBitflag;
		set => SetModelValue(value, m => m.PoisonBitflag, (m, v) => m.PoisonBitflag = v);
	}

	public bool SleepBitflag
	{
		get => Model.SleepBitflag;
		set => SetModelValue(value, m => m.SleepBitflag, (m, v) => m.SleepBitflag = v);
	}

	public bool ParalysisBitflag
	{
		get => Model.ParalysisBitflag;
		set => SetModelValue(value, m => m.ParalysisBitflag, (m, v) => m.ParalysisBitflag = v);
	}

	public bool ConfusionBitflag
	{
		get => Model.ConfusionBitflag;
		set => SetModelValue(value, m => m.ConfusionBitflag, (m, v) => m.ConfusionBitflag = v);
	}

	public bool PlagueBitflag
	{
		get => Model.PlagueBitflag;
		set => SetModelValue(value, m => m.PlagueBitflag, (m, v) => m.PlagueBitflag = v);
	}

	public bool MagicBlockBitflag
	{
		get => Model.MagicBlockBitflag;
		set => SetModelValue(value, m => m.MagicBlockBitflag, (m, v) => m.MagicBlockBitflag = v);
	}

	public bool MoveBlockBitflag
	{
		get => Model.MoveBlockBitflag;
		set => SetModelValue(value, m => m.MoveBlockBitflag, (m, v) => m.MoveBlockBitflag = v);
	}

	public bool DeathBitflag
	{
		get => Model.DeathBitflag;
		set => SetModelValue(value, m => m.DeathBitflag, (m, v) => m.DeathBitflag = v);
	}
}
