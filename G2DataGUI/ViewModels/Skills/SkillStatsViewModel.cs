using G2DataGUI.Domain.Skills;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.ViewModels.Skills;

public sealed class SkillStatsViewModel(SkillStats model, IRepository repository)
	: ItemViewModel<SkillStats>(model, repository)
{
	public byte CostType1
	{
		get => Model.CostType1;
		set => SetModelValue(value, m => m.CostType1, (m, v) => m.CostType1 = v);
	}

	public byte CostType2
	{
		get => Model.CostType2;
		set => SetModelValue(value, m => m.CostType2, (m, v) => m.CostType2 = v);
	}

	public short BaseHP
	{
		get => Model.BaseHP;
		set => SetModelValue(value, m => m.BaseHP, (m, v) => m.BaseHP = v);
	}

	public short BaseMP
	{
		get => Model.BaseMP;
		set => SetModelValue(value, m => m.BaseMP, (m, v) => m.BaseMP = v);
	}

	public short BaseSP
	{
		get => Model.BaseSP;
		set => SetModelValue(value, m => m.BaseSP, (m, v) => m.BaseSP = v);
	}

	public short BaseStrength
	{
		get => Model.BaseStrength;
		set => SetModelValue(value, m => m.BaseStrength, (m, v) => m.BaseStrength = v);
	}

	public short BaseVitality
	{
		get => Model.BaseVitality;
		set => SetModelValue(value, m => m.BaseVitality, (m, v) => m.BaseVitality = v);
	}

	public short BaseAction
	{
		get => Model.BaseAction;
		set => SetModelValue(value, m => m.BaseAction, (m, v) => m.BaseAction = v);
	}

	public short BaseMovement
	{
		get => Model.BaseMovement;
		set => SetModelValue(value, m => m.BaseMovement, (m, v) => m.BaseMovement = v);
	}

	public short BaseMagic
	{
		get => Model.BaseMagic;
		set => SetModelValue(value, m => m.BaseMagic, (m, v) => m.BaseMagic = v);
	}

	public short BaseMentality
	{
		get => Model.BaseMentality;
		set => SetModelValue(value, m => m.BaseMentality, (m, v) => m.BaseMentality = v);
	}

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
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

	public byte Unknown4
	{
		get => Model.Unknown4;
		set => SetModelValue(value, m => m.Unknown4, (m, v) => m.Unknown4 = v);
	}

	public byte Unknown5
	{
		get => Model.Unknown5;
		set => SetModelValue(value, m => m.Unknown5, (m, v) => m.Unknown5 = v);
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

	public byte Special
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
}
