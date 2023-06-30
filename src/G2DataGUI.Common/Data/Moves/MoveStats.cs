using G2DataGUI.IO.Streams;
using System.IO;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Moves;

[StructLayout(LayoutKind.Explicit, Size = 0x30)]
public struct MoveStats
{
    [FieldOffset(0x00)] public short Cost;
    [FieldOffset(0x02)] public byte TargetEffect;
    [FieldOffset(0x03)] public byte TargetType;
    [FieldOffset(0x04)] public short Strength;
    [FieldOffset(0x06)] public short Power;
    [FieldOffset(0x08)] public short Range;
    [FieldOffset(0x0A)] public short Cast1Time;
    [FieldOffset(0x0C)] public short Cast5Time;
    [FieldOffset(0x0E)] public short RecoveryTime;
    [FieldOffset(0x10)] public short Animation;
    [FieldOffset(0x12)] public byte Unknown1;
    [FieldOffset(0x13)] public byte Knockdown;
    [FieldOffset(0x14)] public short IpDamage;
    [FieldOffset(0x16)] public short IpCancelDamage;
    [FieldOffset(0x18)] public short Knockback;
    [FieldOffset(0x1A)] public byte Element;
    [FieldOffset(0x1B)] public byte ElementStrength;
    [FieldOffset(0x1C)] public byte AilmentsBitflag;
    [FieldOffset(0x1D)] public byte AilmentsChance;
    [FieldOffset(0x1E)] public sbyte AttackModifier;
    [FieldOffset(0x1F)] public sbyte DefenseModifier;
    [FieldOffset(0x20)] public sbyte ActionModifier;
    [FieldOffset(0x21)] public sbyte MovementModifier;
    [FieldOffset(0x22)] public short Special;
    [FieldOffset(0x24)] public short CoinCost1;
    [FieldOffset(0x26)] public short CoinCost2;
    [FieldOffset(0x28)] public short CoinCost3;
    [FieldOffset(0x2A)] public short CoinCost4;
    [FieldOffset(0x2C)] public short CoinCost5;
    [FieldOffset(0x2E)] public short Multiplier;
}