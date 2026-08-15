using G2DataGUI.Domain.Specials;
using G2DataGUI.Export.Abstractions;
using G2DataGUI.Locale;

namespace G2DataGUI.Export.Specials;

public sealed class SpecialSetCsvExporter(ILocalizationService locale, SpecialMoveCsvExporter moveExporter) : ICsvExporter<SpecialSet>
{
	public string Header =>
		"Character," + string.Join(",", Enumerable.Repeat(moveExporter.Header, SpecialSet.MovesPerSet));

	public void WriteRow(TextWriter writer, SpecialSet item)
	{
		writer.Write($"{locale[LocaleKeys.SpecialChars][item.Id]},");

		for (int i = 0; i < item.Moves.Count; i++)
		{
			if (i > 0)
			{
				writer.Write(',');
			}

			moveExporter.WriteFields(writer, item.Moves[i]);
		}
	}
}
