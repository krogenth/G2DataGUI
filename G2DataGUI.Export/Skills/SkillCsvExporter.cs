using G2DataGUI.Domain.Skills;
using G2DataGUI.Export.Abstractions;

namespace G2DataGUI.Export.Skills;

public sealed class SkillCsvExporter(SkillStatsCsvExporter statsExporter) : ICsvExporter<Skill>
{
	public string Header => $"Name,{statsExporter.Header},Description";

	public void WriteRow(TextWriter writer, Skill item)
	{
		writer.Write($"{item.Name},");
		statsExporter.WriteFields(writer, item.Stats);
		writer.WriteLine($",{item.Description}");
	}
}
