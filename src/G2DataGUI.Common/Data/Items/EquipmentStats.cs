using G2DataGUI.IO.Streams;
using System.IO;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Items;

[StructLayout(LayoutKind.Explicit, Size = 0x1C)]
public struct EquipmentStats
{
    [FieldOffset(0x00)] public ushort CharacterBitflag;
    [FieldOffset(0x02)] public short Strength;
    [FieldOffset(0x04)] public short Vitality;
    [FieldOffset(0x06)] public short Action;
    [FieldOffset(0x08)] public short Movement;
    [FieldOffset(0x0A)] public byte EffectiveOn;
    [FieldOffset(0x0B)] public sbyte FireAffinity;
    [FieldOffset(0x0C)] public sbyte WindAffinity;
    [FieldOffset(0x0D)] public sbyte EarthAffinity;
    [FieldOffset(0x0E)] public sbyte LightningAffinity;
    [FieldOffset(0x0F)] public sbyte BlizzardAffinity;
    [FieldOffset(0x10)] public byte AilmentsBitflag;
    [FieldOffset(0x11)] public byte AilmentsChance;
    [FieldOffset(0x12)] public sbyte IncreaseFirePercent;
    [FieldOffset(0x13)] public sbyte IncreaseWindPercent;
    [FieldOffset(0x14)] public sbyte IncreaseEarthPercent;
    [FieldOffset(0x15)] public sbyte IncreaseLightningPercent;
    [FieldOffset(0x16)] public sbyte IncreaseBlizzardPercent;
    [FieldOffset(0x17)] public sbyte IncreaseWaterPercent;
    [FieldOffset(0x18)] public sbyte IncreaseExplosionPercent;
    [FieldOffset(0x19)] public sbyte IncreaseForestPercent;
    [FieldOffset(0x1A)] public short Special;
}