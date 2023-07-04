using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Moves;

public class MoveStats
{
    public short Cost { get; set; }
    public byte TargetEffect { get; set; }
    public byte TargetType { get; set; }
    public short Strength { get; set; }
    public short Power { get; set; }
    public short Range { get; set; }
    public short Cast1Time { get; set; }
    public short Cast5Time { get; set; }
    public short RecoveryTime { get; set; }
    public short Animation { get; set; }
    public byte Unknown1 { get; set; }
    public byte Knockdown { get; set; }
    public short IpDamage { get; set; }
    public short IpCancelDamage { get; set; }
    public short Knockback { get; set; }
    public byte Element { get; set; }
    public byte ElementStrength { get; set; }
    public byte AilmentsBitflag { get; set; }
    public byte AilmentsChance { get; set; }
    public sbyte AttackModifier { get; set; }
    public sbyte DefenseModifier { get; set; }
    public sbyte ActionModifier { get; set; }
    public sbyte MovementModifier { get; set; }
    public short Special { get; set; }
    public short CoinCost1 { get; set; }
    public short CoinCost2 { get; set; }
    public short CoinCost3 { get; set; }
    public short CoinCost4 { get; set; }
    public short CoinCost5 { get; set; }
    public short Multiplier { get; set; }

    public bool PoisonBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Poison) > 0;
        set
        {
            if (value) AilmentsBitflag |= (byte)Ailments.AilmentTypes.Poison;
            else AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Poison;
        }
    }
    public bool SleepBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Sleep) > 0;
        set
        {
            if (value) AilmentsBitflag |= (byte)Ailments.AilmentTypes.Sleep;
            else AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Sleep;
        }
    }
    public bool ParalysisBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Paralysis) > 0;
        set
        {
            if (value) AilmentsBitflag |= (byte)Ailments.AilmentTypes.Paralysis;
            else AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Paralysis;
        }
    }
    public bool ConfusionBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Confusion) > 0;
        set
        {
            if (value) AilmentsBitflag |= (byte)Ailments.AilmentTypes.Confusion;
            else AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Confusion;
        }
    }
    public bool PlagueBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Plague) > 0;
        set
        {
            if (value) AilmentsBitflag |= (byte)Ailments.AilmentTypes.Plague;
            else AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Plague;
        }
    }
    public bool Magic_BlockBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Magic_Block) > 0;
        set
        {
            if (value) AilmentsBitflag |= (byte)Ailments.AilmentTypes.Magic_Block;
            else AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Magic_Block;
        }
    }
    public bool Move_BlockBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Move_Block) > 0;
        set
        {
            if (value) AilmentsBitflag |= (byte)Ailments.AilmentTypes.Move_Block;
            else AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Move_Block;
        }
    }
    public bool DeathBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Death) > 0;
        set
        {
            if (value) AilmentsBitflag |= (byte)Ailments.AilmentTypes.Death;
            else AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Death;
        }
    }

    public static MoveStats ReadMoveStats(Stream reader)
    {
        MoveStats stats = new MoveStats();
        stats.Cost = reader.ReadRawShort();
        stats.TargetEffect = reader.ReadRawByte();
        stats.TargetType = reader.ReadRawByte();
        stats.Strength = reader.ReadRawShort();
        stats.Power = reader.ReadRawShort();
        stats.Range = reader.ReadRawShort();
        stats.Cast1Time = reader.ReadRawShort();
        stats.Cast5Time = reader.ReadRawShort();
        stats.RecoveryTime = reader.ReadRawShort();
        stats.Animation = reader.ReadRawShort();
        stats.Unknown1 = reader.ReadRawByte();
        stats.Knockdown = reader.ReadRawByte();
        stats.IpDamage = reader.ReadRawShort();
        stats.IpCancelDamage = reader.ReadRawShort();
        stats.Knockback = reader.ReadRawShort();
        stats.Element = reader.ReadRawByte();
        stats.ElementStrength = reader.ReadRawByte();
        stats.AilmentsBitflag = reader.ReadRawByte();
        stats.AilmentsChance = reader.ReadRawByte();
        stats.AttackModifier = reader.ReadRawSByte();
        stats.DefenseModifier = reader.ReadRawSByte();
        stats.ActionModifier = reader.ReadRawSByte();
        stats.MovementModifier = reader.ReadRawSByte();
        stats.Special = reader.ReadRawShort();
        stats.CoinCost1 = reader.ReadRawShort();
        stats.CoinCost2 = reader.ReadRawShort();
        stats.CoinCost3 = reader.ReadRawShort();
        stats.CoinCost4 = reader.ReadRawShort();
        stats.CoinCost5 = reader.ReadRawShort();
        stats.Multiplier = reader.ReadRawShort();

        return stats;
    }

    public void WriteMoveStats(Stream writer)
    {
        writer.WriteRawShort(Cost);
        writer.WriteRawByte(TargetEffect);
        writer.WriteRawByte(TargetType);
        writer.WriteRawShort(Strength);
        writer.WriteRawShort(Power);
        writer.WriteRawShort(Range);
        writer.WriteRawShort(Cast1Time);
        writer.WriteRawShort(Cast5Time);
        writer.WriteRawShort(RecoveryTime);
        writer.WriteRawShort(Animation);
        writer.WriteRawByte(Unknown1);
        writer.WriteRawByte(Knockdown);
        writer.WriteRawShort(IpDamage);
        writer.WriteRawShort(IpCancelDamage);
        writer.WriteRawShort(Knockback);
        writer.WriteRawByte(Element);
        writer.WriteRawByte(ElementStrength);
        writer.WriteRawByte(AilmentsBitflag);
        writer.WriteRawByte(AilmentsChance);
        writer.WriteRawSByte(AttackModifier);
        writer.WriteRawSByte(DefenseModifier);
        writer.WriteRawSByte(ActionModifier);
        writer.WriteRawSByte(MovementModifier);
        writer.WriteRawShort(Special);
        writer.WriteRawShort(CoinCost1);
        writer.WriteRawShort(CoinCost2);
        writer.WriteRawShort(CoinCost3);
        writer.WriteRawShort(CoinCost4);
        writer.WriteRawShort(CoinCost5);
        writer.WriteRawShort(Multiplier);
    }
}