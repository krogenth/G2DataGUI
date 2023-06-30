using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Items;

[StructLayout(LayoutKind.Explicit, Size = 0x20)]
public struct UsableStats
{
    [FieldOffset(0x00)] public byte TargetEffect;
    [FieldOffset(0x01)] public byte TargetType;
    [FieldOffset(0x02)] public short Power;
    [FieldOffset(0x04)] public short Range;
    [FieldOffset(0x06)] public short CastTime;
    [FieldOffset(0x08)] public short RecoveryTime;
    [FieldOffset(0x0A)] public ushort Animation;
    [FieldOffset(0x0C)] public byte EffectiveOn;
    [FieldOffset(0x0D)] public byte Unknown1;
    [FieldOffset(0x0E)] public short IpDamage;
    [FieldOffset(0x10)] public short IpCancelDamage;
    [FieldOffset(0x12)] public short Knockback;
    [FieldOffset(0x14)] public byte Element;
    [FieldOffset(0x15)] public byte ElementStrength;
    [FieldOffset(0x16)] public byte AilmentsBitflag;
    [FieldOffset(0x17)] public byte AilmentsChance;
    [FieldOffset(0x18)] public sbyte AttackModifier;
    [FieldOffset(0x19)] public sbyte DefenseModifier;
    [FieldOffset(0x1A)] public sbyte ActionModifier;
    [FieldOffset(0x1B)] public sbyte MovementModifier;
    [FieldOffset(0x1C)] public byte BreakChance;
    [FieldOffset(0x1D)] public byte Special;
    [FieldOffset(0x1E)] public byte Unknown2;
    [FieldOffset(0x1F)] public byte Unknown3;
}