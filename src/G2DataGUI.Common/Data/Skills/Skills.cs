using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class Skills
{
    public static Skills Instance { get; } = new();
    private ObservableCollection<Skill> _skills = new();
    public event EventHandler CollectionRefreshed;

    private Skills()
    {
        ReadSkills();
    }

    public void Save()
    {
        WriteSkills();
    }

    public void Reload()
    {
        ReadSkills();
    }

    private void ReadSkills()
    {
        _skills.Clear();
        using (FileStream reader = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.Skills, FileMode.Open))
        using(MemoryStream memReader = new MemoryStream())
        {
            reader.CopyTo(memReader);
            memReader.Seek(0, SeekOrigin.Begin);
            while (memReader.Position < memReader.Length)
            {
                Skill skill = Skill.ReadSkill(memReader);
                if (skill != null)
                {
                    _skills.Add(skill);
                }
            }
        }

        CollectionRefreshed?.Invoke(this, EventArgs.Empty);
    }

    private void WriteSkills()
    {
        using (FileStream writer = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.Skills, FileMode.OpenOrCreate))
        {
            foreach (Skill skill in _skills)
            {
                skill.WriteSkill(writer);
            }
        }
    }

    public ObservableCollection<Skill> GetSkills() { return _skills; }
}