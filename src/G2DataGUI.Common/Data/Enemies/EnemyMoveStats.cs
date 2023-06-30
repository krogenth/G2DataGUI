using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Enemies;

[StructLayout(LayoutKind.Explicit, Size = 0x2A)]
public struct EnemyMoveStats
{
    [FieldOffset(0x00)] public short mp;
    [FieldOffset(0x02)] public short sp;
    [FieldOffset(0x04)] public byte unknown1;
    [FieldOffset(0x05)] public byte targetEffect;
    [FieldOffset(0x06)] public short strength;
    [FieldOffset(0x08)] public short power;
    [FieldOffset(0x0A)] public short ad;
    [FieldOffset(0x0C)] public byte targetType;
    [FieldOffset(0x0D)] public byte normalAttackFlag;
    [FieldOffset(0x0E)] public short distance;
    [FieldOffset(0x10)] public short accuracy;
    [FieldOffset(0x12)] public short range;
    [FieldOffset(0x14)] public short castTime;
    [FieldOffset(0x16)] public short recovery;
    [FieldOffset(0x18)] public byte animation;
    [FieldOffset(0x19)] public byte knockdown;
    [FieldOffset(0x1A)] public short ipStun;
    [FieldOffset(0x1C)] public short ipCancelStun;
    [FieldOffset(0x1E)] public short knockback;
    [FieldOffset(0x20)] public byte element;
    [FieldOffset(0x21)] public byte elementStrength;
    [FieldOffset(0x22)] public byte ailmentsBitflag;
    [FieldOffset(0x23)] public byte ailmentsChance;
    [FieldOffset(0x24)] public byte attackModifier;
    [FieldOffset(0x25)] public byte defenseModifier;
    [FieldOffset(0x26)] public byte actionModifier;
    [FieldOffset(0x27)] public byte movementModifier;
    [FieldOffset(0x28)] public short special;
}