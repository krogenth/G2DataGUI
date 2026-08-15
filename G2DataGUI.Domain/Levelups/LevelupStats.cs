namespace G2DataGUI.Domain.Levelups;

public sealed class LevelupStats
{
	/// <summary>
	/// 1-based level this row applies to (position within the
	/// character's 99-row block, not read from the file).
	/// </summary>
	public int Level { get; set; }

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
}
