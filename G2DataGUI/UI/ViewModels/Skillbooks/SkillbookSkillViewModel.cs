using System.Collections.ObjectModel;
using System.Linq;
using G2DataGUI.Domain.Skills;
using G2DataGUI.Repositories.Abstractions;
using G2DataGUI.UI.Common.ViewModels;
using G2DataGUI.UI.ViewModels.Skills;

namespace G2DataGUI.UI.ViewModels.Skillbooks;

/// <summary>Same "reference by name, not raw offset" pattern as
/// ManaeggSpellViewModel.SelectedMove — see its doc comment. AvailableSkills
/// is the same ObservableCollection SkillsViewModel.Items already is.</summary>
public sealed class SkillbookSkillViewModel(SkillbookSkill model, IRepository repository, ObservableCollection<SkillViewModel> availableSkills)
	: ItemViewModel<SkillbookSkill>(model, repository)
{
	public ObservableCollection<SkillViewModel> AvailableSkills => availableSkills;

	public SkillViewModel? SelectedSkill
	{
		get => AvailableSkills.FirstOrDefault(skill => skill.Id == SkillOffset);
		set
		{
			if (value is null)
			{
				return;
			}

			SetModelValue((byte)value.Id, m => m.SkillOffset, (m, v) => m.SkillOffset = v, nameof(SkillOffset));
			OnPropertyChanged(nameof(SelectedSkill));
		}
	}

	public byte SkillOffset
	{
		get => Model.SkillOffset;
		set
		{
			SetModelValue(value, m => m.SkillOffset, (m, v) => m.SkillOffset = v);
			OnPropertyChanged(nameof(SelectedSkill));
		}
	}

	public byte StartingLevel
	{
		get => Model.StartingLevel;
		set => SetModelValue(value, m => m.StartingLevel, (m, v) => m.StartingLevel = v);
	}

	public byte RequiredLevel
	{
		get => Model.RequiredLevel;
		set => SetModelValue(value, m => m.RequiredLevel, (m, v) => m.RequiredLevel = v);
	}

	public byte Unknown1
	{
		get => Model.Unknown1;
		set => SetModelValue(value, m => m.Unknown1, (m, v) => m.Unknown1 = v);
	}
}
