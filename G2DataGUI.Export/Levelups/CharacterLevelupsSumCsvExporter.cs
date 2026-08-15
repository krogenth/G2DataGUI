using G2DataGUI.Domain.Levelups;
using G2DataGUI.Export.Abstractions;
using G2DataGUI.Locale;

namespace G2DataGUI.Export.Levelups;

/// <summary>
/// Cumulative rows: each row is the running total up to and
/// including that level, reset back to zero at the start of each character.
/// Matches the old LEVELUPS_SUM.CSV.
/// </summary>
public sealed class CharacterLevelupsSumCsvExporter(ILocalizationService locale, LevelupStatsCsvExporter statsExporter) : ICsvExporter<CharacterLevelups>
{
	public string Header => $"Character,{statsExporter.Header}";

	public void WriteRow(TextWriter writer, CharacterLevelups item)
	{
		string characterName = locale[LocaleKeys.Characters][item.Id];
		LevelupStats runningTotal = new();

		foreach (LevelupStats level in item.Levels)
		{
			runningTotal = Add(runningTotal, level);

			writer.Write($"{characterName},");
			statsExporter.WriteFields(writer, runningTotal);
			writer.WriteLine();
		}
	}

	private static LevelupStats Add(LevelupStats total, LevelupStats level) => new()
	{
		Level = level.Level,
		Experience = total.Experience + level.Experience,
		IncreaseHP = (short)(total.IncreaseHP + level.IncreaseHP),
		IncreaseMP = (short)(total.IncreaseMP + level.IncreaseMP),
		IncreaseSP = (short)(total.IncreaseSP + level.IncreaseSP),
		IncreaseStrength = (short)(total.IncreaseStrength + level.IncreaseStrength),
		IncreaseVitality = (short)(total.IncreaseVitality + level.IncreaseVitality),
		IncreaseAction = (short)(total.IncreaseAction + level.IncreaseAction),
		IncreaseMovement = (short)(total.IncreaseMovement + level.IncreaseMovement),
		IncreaseMagic = (short)(total.IncreaseMagic + level.IncreaseMagic),
		IncreaseMentality = (short)(total.IncreaseMentality + level.IncreaseMentality),
		IncreaseSkillSlot = (short)(total.IncreaseSkillSlot + level.IncreaseSkillSlot),
	};
}
