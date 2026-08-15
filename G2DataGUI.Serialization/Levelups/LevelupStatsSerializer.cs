using G2DataGUI.Domain.Levelups;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Levelups;

public static class LevelupStatsSerializer
{
	public static LevelupStats Read(Stream stream, int level) => new()
	{
		Level = level,
		Experience = stream.ReadRawInt(),
		IncreaseHP = stream.ReadRawShort(),
		IncreaseMP = stream.ReadRawShort(),
		IncreaseSP = stream.ReadRawShort(),
		IncreaseStrength = stream.ReadRawShort(),
		IncreaseVitality = stream.ReadRawShort(),
		IncreaseAction = stream.ReadRawShort(),
		IncreaseMovement = stream.ReadRawShort(),
		IncreaseMagic = stream.ReadRawShort(),
		IncreaseMentality = stream.ReadRawShort(),
		IncreaseSkillSlot = stream.ReadRawShort(),
	};

	public static void Write(Stream stream, LevelupStats stats)
	{
		stream.WriteRawInt(stats.Experience);
		stream.WriteRawShort(stats.IncreaseHP);
		stream.WriteRawShort(stats.IncreaseMP);
		stream.WriteRawShort(stats.IncreaseSP);
		stream.WriteRawShort(stats.IncreaseStrength);
		stream.WriteRawShort(stats.IncreaseVitality);
		stream.WriteRawShort(stats.IncreaseAction);
		stream.WriteRawShort(stats.IncreaseMovement);
		stream.WriteRawShort(stats.IncreaseMagic);
		stream.WriteRawShort(stats.IncreaseMentality);
		stream.WriteRawShort(stats.IncreaseSkillSlot);
	}
}
