using System;
using System.Collections.ObjectModel;
using System.IO;

namespace G2DataGUI.Common.Data.Skills;

public class Skills
{
    public static Skills Instance { get; } = new();
    private ObservableCollection<Skill> _skills = new();
    public event EventHandler CollectionRefreshed;
    public static int NumberOfSkills = 0x80;

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
        using (FileStream reader = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.SkillsPath, FileMode.Open, FileAccess.Read))
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
        using (FileStream writer = File.Open(Version.Instance.RootDataDirectory + GameFilePaths.SkillsPath, FileMode.OpenOrCreate, FileAccess.ReadWrite))
        {
            foreach (Skill skill in _skills)
            {
                skill.WriteSkill(writer);
            }
        }
    }

    public ObservableCollection<Skill> GetSkills() { return _skills; }
}