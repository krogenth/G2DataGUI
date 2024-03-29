﻿using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using G2DataGUI.Common.Paths;

namespace G2DataGUI.Common.Data.Skills;

public class Skills
{
    public static Skills Instance { get; } = new();
    public ObservableCollection<Skill> GameSkills { get; private set; } = new();
    public event EventHandler CollectionRefreshed;
    public static int NumberOfSkills = 0x80;

    private Skills()
    {
        _ = ReadSkillsAsync();
    }

	public void Save() => WriteSkills();

	public void Reload() => _ = ReadSkillsAsync();

	private async Task ReadSkillsAsync() =>
		await Task.Run(ReadSkills).ConfigureAwait(false);

	private void ReadSkills()
    {
		try
		{
			GameSkills.Clear();
			using FileStream reader = File.Open(
				Version.Instance.RootDataDirectory + GamePaths.SkillsPath,
				FileMode.Open,
				FileAccess.Read);
			using MemoryStream memReader = new();
			reader.CopyTo(memReader);
			memReader.Seek(0, SeekOrigin.Begin);
			while (memReader.Position < memReader.Length)
			{
				GameSkills.Add(Skill.ReadSkill(memReader));
			}

			CollectionRefreshed?.Invoke(this, EventArgs.Empty);
		}
        catch (Exception ex)
		{
			Errors.Errors.Instance.AddError("Skills", ex.Message);
		}
    }

    private void WriteSkills()
    {
        using FileStream writer = File.Open(
            Version.Instance.RootDataDirectory + GamePaths.SkillsPath,
            FileMode.OpenOrCreate,
            FileAccess.ReadWrite);
        foreach (Skill skill in GameSkills)
        {
            skill.WriteSkill(writer);
        }
    }

	public void GenerateCSV()
	{
		using FileStream stream = File.Open(ProjectPaths.SkillsCSVPath, FileMode.Create, FileAccess.Write);
		using StreamWriter writer = new(stream);
		writer.WriteLine($"{Skill.CSVHeader}");
		foreach (var skill in  GameSkills)
		{
			skill.GenerateCSV(writer);
		}
	}
}
