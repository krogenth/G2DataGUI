using System;

namespace G2DataGUI.Common.Data.Common;

public class Ailments
{
    [Flags]
    public enum AilmentTypes : byte
    {
        Poison      = 0b00000001,
        Sleep       = 0b00000010,
        Paralysis   = 0b00000100,
        Confusion   = 0b00001000,
        Plague      = 0b00010000,
        Magic_Block = 0b00100000,
        Move_Block  = 0b01000000,
        Death       = 0b10000000,
    }
}