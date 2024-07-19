using G2DataGUI.Common.Data.Skills;
using System;
using System.Collections.ObjectModel;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public sealed class SkillsViewModel : BaseViewModel
{
	public ObservableCollection<Skill> GameSkills { get; set; } = Skills.Instance.GameSkills;
	private int _selectedSkillIndex = 0;
	public Skill? SelectedSkillItem { get; set; }

	public SkillsViewModel()
	{
		SelectedSkillIndex = 0;
		Skills.Instance.CollectionRefreshed += SkillsCollectionRefreshed;
	}

	private void SkillsCollectionRefreshed(object? sender, EventArgs _) =>
		SelectedSkillIndex = SelectedSkillIndex;

	public int SelectedSkillIndex
	{
		get => _selectedSkillIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (GameSkills.Count == 0)
			{
				return;
			}

			_selectedSkillIndex = value;
			SelectedSkillItem = GameSkills[value];
			OnPropertyChanged(nameof(SelectedSkillIndex));
			OnPropertyChanged(nameof(SelectedSkillItem));
		}
	}
}
