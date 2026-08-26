using G2DataGUI.Domain.Common;
using G2DataGUI.Domain.Items;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.Common.ViewModels.Flags;

namespace G2DataGUI.UI.ViewModels.Items;

/// <summary>
/// CharacterBitflag/AilmentsBitflag stay as raw editable fields here —
/// Equipment.CharacterFlag(CharacterTypes)/AilmentFlag(AilmentTypes) are
/// method-based (not per-flag properties like MoveStats), because
/// CharacterTypes/AilmentTypes have too many members to hand-write one
/// bindable property per flag the way MoveStatsViewModel does for
/// ailments. A per-flag checkbox list would enumerate the enum and call
/// those methods instead — not built out in this pass.
/// </summary>
public sealed class EquipmentViewModel
	: ItemViewModel<Equipment>
{
	public CharacterFlagsSelectorViewModel Characters { get; }
	public AilmentsFlagsSelectorViewModel Ailments { get; }

	public EquipmentViewModel(Equipment model, IRepository repository) : base(model, repository)
	{
		Characters = new("Characters Equippable", (CharacterTypes)model.CharacterBitflag);
		Characters.FlagsChanged += (_, e) =>
			SetModelValue((byte)e.Value, m => m.CharacterBitflag, (m, v) => m.CharacterBitflag = v, nameof(CharacterBitflag));

		Ailments = new AilmentsFlagsSelectorViewModel("Ailments Inflicted/Resisted", (AilmentTypes)model.AilmentsBitflag);
		Ailments.FlagsChanged += (_, e) =>
			SetModelValue((byte)e.Value, m => m.AilmentsBitflag, (m, v) => m.AilmentsBitflag = v, nameof(AilmentsBitflag));
	}

	public ushort CharacterBitflag
	{
		get => Model.CharacterBitflag;
		set => SetModelValue(value, m => m.CharacterBitflag, (m, v) => m.CharacterBitflag = v);
	}

	public short Strength
	{
		get => Model.Strength;
		set => SetModelValue(value, m => m.Strength, (m, v) => m.Strength = v);
	}

	public short Vitality
	{
		get => Model.Vitality;
		set => SetModelValue(value, m => m.Vitality, (m, v) => m.Vitality = v);
	}

	public short Action
	{
		get => Model.Action;
		set => SetModelValue(value, m => m.Action, (m, v) => m.Action = v);
	}

	public short Movement
	{
		get => Model.Movement;
		set => SetModelValue(value, m => m.Movement, (m, v) => m.Movement = v);
	}

	public byte EffectiveOn
	{
		get => Model.EffectiveOn;
		set => SetModelValue(value, m => m.EffectiveOn, (m, v) => m.EffectiveOn = v);
	}

	public sbyte FireAffinity
	{
		get => Model.FireAffinity;
		set => SetModelValue(value, m => m.FireAffinity, (m, v) => m.FireAffinity = v);
	}

	public sbyte WindAffinity
	{
		get => Model.WindAffinity;
		set => SetModelValue(value, m => m.WindAffinity, (m, v) => m.WindAffinity = v);
	}

	public sbyte EarthAffinity
	{
		get => Model.EarthAffinity;
		set => SetModelValue(value, m => m.EarthAffinity, (m, v) => m.EarthAffinity = v);
	}

	public sbyte LightningAffinity
	{
		get => Model.LightningAffinity;
		set => SetModelValue(value, m => m.LightningAffinity, (m, v) => m.LightningAffinity = v);
	}

	public sbyte BlizzardAffinity
	{
		get => Model.BlizzardAffinity;
		set => SetModelValue(value, m => m.BlizzardAffinity, (m, v) => m.BlizzardAffinity = v);
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

	public sbyte IncreaseFirePercent
	{
		get => Model.IncreaseFirePercent;
		set => SetModelValue(value, m => m.IncreaseFirePercent, (m, v) => m.IncreaseFirePercent = v);
	}

	public sbyte IncreaseWindPercent
	{
		get => Model.IncreaseWindPercent;
		set => SetModelValue(value, m => m.IncreaseWindPercent, (m, v) => m.IncreaseWindPercent = v);
	}

	public sbyte IncreaseEarthPercent
	{
		get => Model.IncreaseEarthPercent;
		set => SetModelValue(value, m => m.IncreaseEarthPercent, (m, v) => m.IncreaseEarthPercent = v);
	}

	public sbyte IncreaseLightningPercent
	{
		get => Model.IncreaseLightningPercent;
		set => SetModelValue(value, m => m.IncreaseLightningPercent, (m, v) => m.IncreaseLightningPercent = v);
	}

	public sbyte IncreaseBlizzardPercent
	{
		get => Model.IncreaseBlizzardPercent;
		set => SetModelValue(value, m => m.IncreaseBlizzardPercent, (m, v) => m.IncreaseBlizzardPercent = v);
	}

	public sbyte IncreaseWaterPercent
	{
		get => Model.IncreaseWaterPercent;
		set => SetModelValue(value, m => m.IncreaseWaterPercent, (m, v) => m.IncreaseWaterPercent = v);
	}

	public sbyte IncreaseExplosionPercent
	{
		get => Model.IncreaseExplosionPercent;
		set => SetModelValue(value, m => m.IncreaseExplosionPercent, (m, v) => m.IncreaseExplosionPercent = v);
	}

	public sbyte IncreaseForestPercent
	{
		get => Model.IncreaseForestPercent;
		set => SetModelValue(value, m => m.IncreaseForestPercent, (m, v) => m.IncreaseForestPercent = v);
	}

	public short Special
	{
		get => Model.Special;
		set => SetModelValue(value, m => m.Special, (m, v) => m.Special = v);
	}
}
