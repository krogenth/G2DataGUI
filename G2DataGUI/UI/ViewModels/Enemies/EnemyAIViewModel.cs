using G2DataGUI.Domain.Enemies;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels.Enemies;

public sealed class EnemyAIViewModel(EnemyAI model, IRepository repository)
	: ItemViewModel<EnemyAI>(model, repository)
{
	public byte Condition
	{
		get => Model.Condition;
		set => SetModelValue(value, m => m.Condition, (m, v) => m.Condition = v);
	}

	public byte Move1Chance
	{
		get => Model.Move1Chance;
		set => SetModelValue(value, m => m.Move1Chance, (m, v) => m.Move1Chance = v);
	}

	public byte Move2Chance
	{
		get => Model.Move2Chance;
		set => SetModelValue(value, m => m.Move2Chance, (m, v) => m.Move2Chance = v);
	}

	public byte Move3Chance
	{
		get => Model.Move3Chance;
		set => SetModelValue(value, m => m.Move3Chance, (m, v) => m.Move3Chance = v);
	}

	public byte Move4Chance
	{
		get => Model.Move4Chance;
		set => SetModelValue(value, m => m.Move4Chance, (m, v) => m.Move4Chance = v);
	}

	public byte Move5Chance
	{
		get => Model.Move5Chance;
		set => SetModelValue(value, m => m.Move5Chance, (m, v) => m.Move5Chance = v);
	}
}
