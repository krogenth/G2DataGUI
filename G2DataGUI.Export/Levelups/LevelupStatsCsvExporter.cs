using G2DataGUI.Domain.Levelups;

namespace G2DataGUI.Export.Levelups;

public sealed class LevelupStatsCsvExporter
{
	public string Header =>
		"Experience,HP Increase,MP Increase,SP Increase,Strength Increase,Vitality Increase,Action Increase," +
		"Movement Increase,Magic Increase,Mentality Increase,Skillslot Increase";

	public void WriteFields(TextWriter writer, LevelupStats stats)
	{
		writer.Write(
			$"{stats.Experience}," +
			$"{stats.IncreaseHP}," +
			$"{stats.IncreaseMP}," +
			$"{stats.IncreaseSP}," +
			$"{stats.IncreaseStrength}," +
			$"{stats.IncreaseVitality}," +
			$"{stats.IncreaseAction}," +
			$"{stats.IncreaseMovement}," +
			$"{stats.IncreaseMagic}," +
			$"{stats.IncreaseMentality}," +
			$"{stats.IncreaseSkillSlot}");
	}
}
