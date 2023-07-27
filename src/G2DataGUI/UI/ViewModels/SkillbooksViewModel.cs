using G2DataGUI.Common.Data.Skills;
using System;
using System.Collections.ObjectModel;
using G2DataGUI.UI.Common.ViewModels;

namespace G2DataGUI.UI.ViewModels;

public class SkillbooksViewModel : BaseViewModel
{
    public ObservableCollection<Skillbook> GameSkillbooks { get; private set; } = Skillbooks.Instance.GameSkillbooks;
    public ObservableCollection<Skill> GameSkills { get; private set; } = Skills.Instance.GameSkills;
    private int _selectedSkillbookIndex;
    private Skillbook _selectedSkillbookItem;

    public static SkillbooksViewModel Instance { get; private set; } = new();

    private SkillbooksViewModel()
    {
		SelectedSkillbookIndex = 0;
		Skillbooks.Instance.CollectionRefreshed += SkillbookCollectionRefreshed;
    }

	private void SkillbookCollectionRefreshed(object sender, EventArgs _) =>
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

			if (value >= GameSkillbooks.Count)
			{
				return;
			}

			_selectedSkillbookIndex = value;
            SelectedSkillbookItem = GameSkillbooks[value];
            OnPropertyChanged(nameof(SelectedSkillbookIndex));
        }
    }

    public Skillbook SelectedSkillbookItem
    {
        get => _selectedSkillbookItem;
        set
        {
            _selectedSkillbookItem = value;
            OnPropertyChanged(nameof(SelectedSkillbookItem));
        }
    }
}
