using G2DataGUI.Common.Data.Skills;
using System;
using System.Collections.ObjectModel;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class SkillbooksViewModel : BaseViewModel
{
	public ObservableCollection<Skillbook> GameSkillbooks { get; set; } = Skillbooks.Instance.GameSkillbooks;
	public ObservableCollection<Skill> GameSkills { get; set; } = Skills.Instance.GameSkills;
	private int _selectedSkillbookIndex = 0;
	public Skillbook? SelectedSkillbookItem { get; set; }

	public SkillbooksViewModel()
	{
		SelectedSkillbookIndex = 0;
		Skillbooks.Instance.CollectionRefreshed += SkillbookCollectionRefreshed;
	}

	private void SkillbookCollectionRefreshed(object? sender, EventArgs _) =>
		SelectedSkillbookIndex = SelectedSkillbookIndex;

	public int SelectedSkillbookIndex
	{
		get => _selectedSkillbookIndex;
		set
		{
			if (value < 0)
			{
				value = 0;
			}

			if (GameSkillbooks.Count == 0)
			{
				return;
			}

			_selectedSkillbookIndex = value;
			SelectedSkillbookItem = GameSkillbooks[value];
			OnPropertyChanged(nameof(SelectedSkillbookIndex));
			OnPropertyChanged(nameof(SelectedSkillbookItem));
		}
	}
}
