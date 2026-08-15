using G2DataGUI.Domain.Moves;
using G2DataGUI.Export.Abstractions;
using G2DataGUI.Locale;

namespace G2DataGUI.Export.Moves;

public sealed class ManaeggCsvExporter(ILocalizationService locale, ManaeggSpellCsvExporter spellExporter) : ICsvExporter<Manaegg>
{
	public string Header =>
		$"Manaegg,{string.Join(",", Enumerable.Repeat(spellExporter.Header, Manaegg.SpellCount))}";

	public void WriteRow(TextWriter writer, Manaegg item)
	{
		writer.Write($"{locale[LocaleKeys.Eggs][item.Id]},");

		for (int i = 0; i < item.Spells.Count; i++)
		{
			if (i > 0)
			{
				writer.Write(',');
			}

			spellExporter.WriteFields(writer, item.Spells[i]);
		}

		writer.WriteLine();
	}
}
