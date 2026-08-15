using G2DataGUI.Domain.Moves;
using G2DataGUI.Export.Abstractions;
using G2DataGUI.Locale;

namespace G2DataGUI.Export.Moves;

public sealed class MoveCsvExporter(ILocalizationService locale, MoveStatsCsvExporter statsExporter) : ICsvExporter<Move>
{
	public string Header => $"ID,Icon,Name,{statsExporter.Header},Description";

	public void WriteRow(TextWriter writer, Move item)
	{
		writer.Write($"{item.Id},{locale[LocaleKeys.Icons][item.Icon]},{item.Name},");
		statsExporter.WriteFields(writer, item.Stats);
		writer.WriteLine($",{item.Description}");
	}
}
