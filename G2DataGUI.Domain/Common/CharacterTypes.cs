namespace G2DataGUI.Domain.Common;

[Flags]
public enum CharacterTypes : ushort
{
	Ryudo      = 0b0000_0000_0000_0001,
	Elena      = 0b0000_0000_0000_0010,
	Millenia   = 0b0000_0000_0000_0100,
	Roan       = 0b0000_0000_0000_1000,
	Tio        = 0b0000_0000_0001_0000,
	Mareg      = 0b0000_0000_0010_0000,
	PrinceRoan = 0b0000_0000_0100_0000,
}
