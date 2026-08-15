using G2DataGUI.Domain.Skills;
using G2DataGUI.Export.Abstractions;
using G2DataGUI.Locale;

namespace G2DataGUI.Export.Skills;

public sealed class SkillbookCsvExporter(ILocalizationService locale, SkillbookSkillCsvExporter skillExporter) : ICsvExporter<Skillbook>
{
	public string Header =>
		$"Skillbook,{string.Join(",", Enumerable.Repeat(skillExporter.Header, Skillbook.SkillCount))}";

	public void WriteRow(TextWriter writer, Skillbook item)
	{
		writer.Write($"{locale[LocaleKeys.Books][item.Id]},");

		for (int i = 0; i < item.Skills.Count; i++)
		{
			if (i > 0)
			{
				writer.Write(',');
			}

			skillExporter.WriteFields(writer, item.Skills[i]);
		}

		writer.WriteLine();
	}
}
