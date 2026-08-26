using G2DataGUI.Domain.Levelups;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Levelups;

public sealed class LevelupStatsViewModel(LevelupStats model, IRepository repository)
	: ItemViewModel<LevelupStats>(model, repository)
{
	public int Level => Model.Level;

	public int Experience
	{
		get => Model.Experience;
		set => SetModelValue(value, m => m.Experience, (m, v) => m.Experience = v);
	}

	public short IncreaseHP
	{
		get => Model.IncreaseHP;
		set => SetModelValue(value, m => m.IncreaseHP, (m, v) => m.IncreaseHP = v);
	}

	public short IncreaseMP
	{
		get => Model.IncreaseMP;
		set => SetModelValue(value, m => m.IncreaseMP, (m, v) => m.IncreaseMP = v);
	}

	public short IncreaseSP
	{
		get => Model.IncreaseSP;
		set => SetModelValue(value, m => m.IncreaseSP, (m, v) => m.IncreaseSP = v);
	}

	public short IncreaseStrength
	{
		get => Model.IncreaseStrength;
		set => SetModelValue(value, m => m.IncreaseStrength, (m, v) => m.IncreaseStrength = v);
	}

	public short IncreaseVitality
	{
		get => Model.IncreaseVitality;
		set => SetModelValue(value, m => m.IncreaseVitality, (m, v) => m.IncreaseVitality = v);
	}

	public short IncreaseAction
	{
		get => Model.IncreaseAction;
		set => SetModelValue(value, m => m.IncreaseAction, (m, v) => m.IncreaseAction = v);
	}

	public short IncreaseMovement
	{
		get => Model.IncreaseMovement;
		set => SetModelValue(value, m => m.IncreaseMovement, (m, v) => m.IncreaseMovement = v);
	}

	public short IncreaseMagic
	{
		get => Model.IncreaseMagic;
		set => SetModelValue(value, m => m.IncreaseMagic, (m, v) => m.IncreaseMagic = v);
	}

	public short IncreaseMentality
	{
		get => Model.IncreaseMentality;
		set => SetModelValue(value, m => m.IncreaseMentality, (m, v) => m.IncreaseMentality = v);
	}

	public short IncreaseSkillSlot
	{
		get => Model.IncreaseSkillSlot;
		set => SetModelValue(value, m => m.IncreaseSkillSlot, (m, v) => m.IncreaseSkillSlot = v);
	}
}
