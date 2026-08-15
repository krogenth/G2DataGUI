using G2DataGUI.Domain.Levelups;
using G2DataGUI.Export.Abstractions;
using G2DataGUI.Locale;

namespace G2DataGUI.Export.Levelups;

/// <summary>Per-level rows: one CSV row per level, character name repeated
/// down the first column. Matches the old LEVELUPS.CSV.</summary>
public sealed class CharacterLevelupsCsvExporter(ILocalizationService locale, LevelupStatsCsvExporter statsExporter) : ICsvExporter<CharacterLevelups>
{
	public string Header => $"Character,{statsExporter.Header}";

	public void WriteRow(TextWriter writer, CharacterLevelups item)
	{
		string characterName = locale[LocaleKeys.Characters][item.Id];

		foreach (LevelupStats level in item.Levels)
		{
			writer.Write($"{characterName},");
			statsExporter.WriteFields(writer, level);
			writer.WriteLine();
		}
	}
}
