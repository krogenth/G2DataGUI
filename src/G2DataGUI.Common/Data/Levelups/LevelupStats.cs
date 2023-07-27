using System.IO;
using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;

namespace G2DataGUI.Common.Data.Levelups;

public class LevelupStats : BaseContainer
{
	// used for UI list rendering
	public int Index { get; private set; }

    public int Experience { get; set; }
	public short IncreaseHP { get; set; }
	public short IncreaseMP { get; set; }
	public short IncreaseSP { get; set; }
	public short IncreaseStrength { get; set; }
	public short IncreaseVitality { get; set; }
	public short IncreaseAction { get; set; }
	public short IncreaseMovement { get; set; }
	public short IncreaseMagic { get; set; }
	public short IncreaseMentality { get; set; }
	public short IncreaseSkillSlot { get; set; }

	public static string CSVHeader =>
		$"Experience,HP Increase,MP Increase,SP Increase,Strength Increase,Vitality Increase,Action Increase," +
		$"Movement Increase,Magic Increase,Mentality Increase,Skillslot Increase";

	public static LevelupStats ReadLevelupStats(Stream reader, int index)
	{
		LevelupStats stats = new()
		{
			Index = index,
			Experience = reader.ReadRawInt(),
			IncreaseHP = reader.ReadRawShort(),
			IncreaseMP = reader.ReadRawShort(),
			IncreaseSP = reader.ReadRawShort(),
			IncreaseStrength = reader.ReadRawShort(),
			IncreaseVitality = reader.ReadRawShort(),
			IncreaseAction = reader.ReadRawShort(),
			IncreaseMovement = reader.ReadRawShort(),
			IncreaseMagic = reader.ReadRawShort(),
			IncreaseMentality = reader.ReadRawShort(),
			IncreaseSkillSlot = reader.ReadRawShort(),
		};
		return stats;
	}

	public void WriteLevelupStats(Stream writer)
	{
		writer.WriteRawInt(Experience);
		writer.WriteRawShort(IncreaseHP);
		writer.WriteRawShort(IncreaseMP);
		writer.WriteRawShort(IncreaseSP);
		writer.WriteRawShort(IncreaseStrength);
		writer.WriteRawShort(IncreaseVitality);
		writer.WriteRawShort(IncreaseAction);
		writer.WriteRawShort(IncreaseMovement);
		writer.WriteRawShort(IncreaseMagic);
		writer.WriteRawShort(IncreaseMentality);
		writer.WriteRawShort(IncreaseSkillSlot);
	}

	public void GenerateCSV(StreamWriter writer)
	{
		writer.Write(
			$"{Experience}," +
			$"{IncreaseHP}," +
			$"{IncreaseMP}," +
			$"{IncreaseSP}," +
			$"{IncreaseStrength}," +
			$"{IncreaseVitality}," +
			$"{IncreaseAction}," +
			$"{IncreaseMovement}," +
			$"{IncreaseMagic}," +
			$"{IncreaseMentality}," +
			$"{IncreaseSkillSlot}");
	}
}
