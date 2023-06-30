using System;

namespace G2DataGUI.Common.Data.Common;

public class Characters
{
    [Flags]
    public enum CharacterTypes : ushort
    {
        Ryudo      = 0b00000000_00000001,
        Elena      = 0b00000000_00000010,
        Millenia   = 0b00000000_00000100,
        Roan       = 0b00000000_00001000,
        Tio        = 0b00000000_00010000,
        Mareg      = 0b00000000_00100000,
        PrinceRoan = 0b00000000_01000000,
    }
}