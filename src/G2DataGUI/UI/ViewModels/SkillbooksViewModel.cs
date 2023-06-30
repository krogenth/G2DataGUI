using G2DataGUI.Common.Data.Skills;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace G2DataGUI.UI.ViewModels;

public class SkillbooksViewModel : BaseViewModel
{
    public ObservableCollection<Skillbook> GameSkillbooks { get; private set; } = Skillbooks.Instance.GetSkillbooks();
    public ObservableCollection<Skill> GameSkills { get; private set; } = Skills.Instance.GetSkills();
    private int _selectedSkillbookIndex = 0;
    private Skillbook _selectedSkillbookItem;

    public static SkillbooksViewModel Instance { get; private set; } = new();

    private SkillbooksViewModel()
    {
        _selectedSkillbookItem = GameSkillbooks.First();
        Skillbooks.Instance.CollectionRefreshed += SkillbookCollectionRefreshed;
    }

    private void SkillbookCollectionRefreshed(object sender, EventArgs _)
    {
        SelectedSkillbookItem = GameSkillbooks.ElementAt(SelectedSkillbookIndex);
    }

    public int SelectedSkillbookIndex
    {
        get => _selectedSkillbookIndex;
        set
        {
            if (value < 0) value = 0;
            if (value >= GameSkillbooks.Count) return;
            _selectedSkillbookIndex = value;
            SelectedSkillbookItem = GameSkillbooks.ElementAt(value);
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