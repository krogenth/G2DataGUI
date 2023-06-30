using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Enemies;

[StructLayout(LayoutKind.Explicit, Size = 0x4E)]
public struct EnemyStats
{
    [FieldOffset(0x00)] public byte Unknown1;
    [FieldOffset(0x01)] public byte Unknown2;
    [FieldOffset(0x02)] public byte Type1;
    [FieldOffset(0x03)] public byte Type2;
    [FieldOffset(0x04)] public short Level;
    [FieldOffset(0x06)] public int Health;
    [FieldOffset(0x0A)] public short Mp;
    [FieldOffset(0x0C)] public short Sp;
    [FieldOffset(0x0E)] public short Vitality;
    [FieldOffset(0x10)] public short Agility;
    [FieldOffset(0x12)] public short Speed;
    [FieldOffset(0x14)] public short Mentality;
    [FieldOffset(0x16)] public short Stamina;
    [FieldOffset(0x18)] public short IpStunResist;
    [FieldOffset(0x1A)] public short IpCancelStunResist;
    [FieldOffset(0x1C)] public byte EvasionStillRate;
    [FieldOffset(0x1D)] public byte EvasionMovingRate;
    [FieldOffset(0x1E)] public byte FireResist;
    [FieldOffset(0x1F)] public byte WindResist;
    [FieldOffset(0x20)] public byte EarthResist;
    [FieldOffset(0x21)] public byte LightningResist;
    [FieldOffset(0x22)] public byte BlizzardResist;
    [FieldOffset(0x23)] public byte AilmentsBitflag;
    [FieldOffset(0x24)] public short KnockbackResist;
    [FieldOffset(0x26)] public short T_REC;
    [FieldOffset(0x28)] public short T_DMG;
    [FieldOffset(0x2A)] public short Unknown3;
    [FieldOffset(0x2C)] public short T_HEAL;
    [FieldOffset(0x2E)] public short Size;
    [FieldOffset(0x30)] public short Unknown4;
    [FieldOffset(0x32)] public byte Unknown5;
    [FieldOffset(0x33)] public byte NoRunFlag;
    [FieldOffset(0x34)] public short Unknown6;
    [FieldOffset(0x36)] public int Experience;
    [FieldOffset(0x3A)] public int SkillCoins;
    [FieldOffset(0x3E)] public int MagicCoins;
    [FieldOffset(0x42)] public int GoldCoins;
    [FieldOffset(0x46)] public short Item1Offset;
    [FieldOffset(0x48)] public short Item2Offset;
    [FieldOffset(0x4A)] public byte Item1Chance;
    [FieldOffset(0x4B)] public byte Item2Chance;
    [FieldOffset(0x4C)] public short Unknown7;
}