namespace G2DataGUI.Domain.Skills;

public sealed class SkillStats
{
	public byte CostType1 { get; set; }
	public byte CostType2 { get; set; }
	public short BaseHP { get; set; }
	public short BaseMP { get; set; }
	public short BaseSP { get; set; }
	public short BaseStrength { get; set; }
	public short BaseVitality { get; set; }
	public short BaseAction { get; set; }
	public short BaseMovement { get; set; }
	public short BaseMagic { get; set; }
	public short BaseMentality { get; set; }
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public byte Unknown5 { get; set; }
	public sbyte IncreaseFirePercent { get; set; }
	public sbyte IncreaseWindPercent { get; set; }
	public sbyte IncreaseEarthPercent { get; set; }
	public sbyte IncreaseLightningPercent { get; set; }
	public sbyte IncreaseBlizzardPercent { get; set; }
	public sbyte IncreaseWaterPercent { get; set; }
	public sbyte IncreaseExplosionPercent { get; set; }
	public sbyte IncreaseForestPercent { get; set; }
	public byte Special { get; set; }
	public short CoinCost1 { get; set; }
	public short CoinCost2 { get; set; }
	public short CoinCost3 { get; set; }
	public short CoinCost4 { get; set; }
	public short CoinCost5 { get; set; }
	public short Multiplier { get; set; }
}
