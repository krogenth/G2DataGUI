namespace G2DataGUI.Domain.Common;

[Flags]
public enum AilmentTypes : byte
{
	Poison     = 0b0000_0001,
	Sleep      = 0b0000_0010,
	Paralysis  = 0b0000_0100,
	Confusion  = 0b0000_1000,
	Plague     = 0b0001_0000,
	MagicBlock = 0b0010_0000,
	MoveBlock  = 0b0100_0000,
	Death      = 0b1000_0000,
}
