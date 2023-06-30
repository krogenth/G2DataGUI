using G2DataGUI.Common.Data.Moves;
using G2DataGUI.IO.Streams;
using System.IO;
using System.Runtime.InteropServices;

namespace G2DataGUI.Common.Data.Skills;

[StructLayout(LayoutKind.Explicit, Size = 0x2E)]
public struct SkillStats
{
    [FieldOffset(0x00)] public byte Cost1;
    [FieldOffset(0x01)] public byte Cost2;
    [FieldOffset(0x02)] public short BaseHP;
    [FieldOffset(0x04)] public short BaseMP;
    [FieldOffset(0x06)] public short BaseSP;
    [FieldOffset(0x08)] public short BaseStrength;
    [FieldOffset(0x0A)] public short BaseVitality;
    [FieldOffset(0x0C)] public short BaseAction;
    [FieldOffset(0x0E)] public short BaseMovement;
    [FieldOffset(0x10)] public short BaseMagic;
    [FieldOffset(0x12)] public short BaseMentality;
    [FieldOffset(0x14)] public byte Unknown1;
    [FieldOffset(0x15)] public byte Unknown2;
    [FieldOffset(0x16)] public byte Unknown3;
    [FieldOffset(0x17)] public byte Unknown4;
    [FieldOffset(0x18)] public byte Unknown5;
    [FieldOffset(0x19)] public sbyte IncreaseFirePercent;
    [FieldOffset(0x1A)] public sbyte IncreaseWindPercent;
    [FieldOffset(0x1B)] public sbyte IncreaseEarthPercent;
    [FieldOffset(0x1C)] public sbyte IncreaseLightningPercent;
    [FieldOffset(0x1D)] public sbyte IncreaseBlizzardPercent;
    [FieldOffset(0x1E)] public sbyte IncreaseWaterPercent;
    [FieldOffset(0x1F)] public sbyte IncreaseExplosionPercent;
    [FieldOffset(0x20)] public sbyte IncreaseForestPercent;
    [FieldOffset(0x21)] public byte Special;
    [FieldOffset(0x22)] public short CoinCost1;
    [FieldOffset(0x24)] public short CoinCost2;
    [FieldOffset(0x26)] public short CoinCost3;
    [FieldOffset(0x28)] public short CoinCost4;
    [FieldOffset(0x2A)] public short CoinCost5;
    [FieldOffset(0x2C)] public short Multiplier;
}