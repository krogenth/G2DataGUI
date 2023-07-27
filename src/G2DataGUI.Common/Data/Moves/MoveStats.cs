using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using G2DataGUI.UI.Common.Locale;
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
    public bool Knockdown { get; set; }
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
            if (value)
            {
                AilmentsBitflag |= (byte)Ailments.AilmentTypes.Poison;
            }
            else
            {
                AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Poison;
            }
        }
    }
    public bool SleepBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Sleep) > 0;
        set
        {
            if (value)
            {
                AilmentsBitflag |= (byte)Ailments.AilmentTypes.Sleep;
            }
            else
            {
                AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Sleep;
            }
        }
    }
    public bool ParalysisBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Paralysis) > 0;
        set
        {
            if (value)
            {
                AilmentsBitflag |= (byte)Ailments.AilmentTypes.Paralysis;
            }
            else
            {
                AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Paralysis;
            }
        }
    }
    public bool ConfusionBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Confusion) > 0;
        set
        {
            if (value)
            {
                AilmentsBitflag |= (byte)Ailments.AilmentTypes.Confusion;
            }
            else
            {
                AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Confusion;
            }
        }
    }
    public bool PlagueBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Plague) > 0;
        set
        {
            if (value)
            {
                AilmentsBitflag |= (byte)Ailments.AilmentTypes.Plague;
            }
            else
            {
                AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Plague;
            }
        }
    }
    public bool Magic_BlockBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Magic_Block) > 0;
        set
        {
            if (value)
            {
                AilmentsBitflag |= (byte)Ailments.AilmentTypes.Magic_Block;
            }
            else
            {
                AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Magic_Block;
            }
        }
    }
    public bool Move_BlockBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Move_Block) > 0;
        set
        {
            if (value)
            {
                AilmentsBitflag |= (byte)Ailments.AilmentTypes.Move_Block;
            }
            else
            {
                AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Move_Block;
            }
        }
    }
    public bool DeathBitflag
    {
        get => (AilmentsBitflag & (byte)Ailments.AilmentTypes.Death) > 0;
        set
        {
            if (value)
            {
                AilmentsBitflag |= (byte)Ailments.AilmentTypes.Death;
            }
            else
            {
                AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Death;
            }
        }
    }

    public static string CSVHeader =>
        "Cost,Target Effect,Target Type,Strength,Power,Range,Cast 1 Time,Cast 5 Time,Recovery Time," +
        "Animation,Unknown #1,Knockdown,IP Damage,IP Cancel Damage,Knockback,Element,Element Strength," +
        "Ailments,Ailments Chance,Attack Modifier,Defense Modifier,Action Modifier,Move Modifier," +
        "Special,Coin Cost Lv1,Coin Cost Lv2,Coin Cost Lv3,Coin Cost Lv4,Coin Cost Lv5,Multiplier";

    public static MoveStats ReadMoveStats(Stream reader)
    {
        MoveStats stats = new()
        {
            Cost = reader.ReadRawShort(),
            TargetEffect = reader.ReadRawByte(),
            TargetType = reader.ReadRawByte(),
            Strength = reader.ReadRawShort(),
            Power = reader.ReadRawShort(),
            Range = reader.ReadRawShort(),
            Cast1Time = reader.ReadRawShort(),
            Cast5Time = reader.ReadRawShort(),
            RecoveryTime = reader.ReadRawShort(),
            Animation = reader.ReadRawShort(),
            Unknown1 = reader.ReadRawByte(),
            Knockdown = reader.ReadRawBool(),
            IpDamage = reader.ReadRawShort(),
            IpCancelDamage = reader.ReadRawShort(),
            Knockback = reader.ReadRawShort(),
            Element = reader.ReadRawByte(),
            ElementStrength = reader.ReadRawByte(),
            AilmentsBitflag = reader.ReadRawByte(),
            AilmentsChance = reader.ReadRawByte(),
            AttackModifier = reader.ReadRawSByte(),
            DefenseModifier = reader.ReadRawSByte(),
            ActionModifier = reader.ReadRawSByte(),
            MovementModifier = reader.ReadRawSByte(),
            Special = reader.ReadRawShort(),
            CoinCost1 = reader.ReadRawShort(),
            CoinCost2 = reader.ReadRawShort(),
            CoinCost3 = reader.ReadRawShort(),
            CoinCost4 = reader.ReadRawShort(),
            CoinCost5 = reader.ReadRawShort(),
            Multiplier = reader.ReadRawShort()
        };

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
        writer.WriteRawBool(Knockdown);
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

    public void GenerateCSV(StreamWriter writer)
    {
        writer.Write(
            $"{Cost}," +
            $"{LocaleManager.Instance[LocaleKeys.TargetEffects][TargetEffect]}," +
            $"{LocaleManager.Instance[LocaleKeys.TargetTypes][TargetType]}," +
            $"{Strength}," +
            $"{Power}," +
            $"{Range}," +
            $"{Cast1Time}," +
            $"{Cast5Time}," +
            $"{RecoveryTime}," +
            $"{LocaleManager.Instance[LocaleKeys.Animations][Animation]}," +
            $"{Unknown1}," +
            $"{Knockdown}," +
            $"{IpDamage}," +
            $"{IpCancelDamage}," +
            $"{Knockback}," +
            $"{LocaleManager.Instance[LocaleKeys.Elements][Element]}," +
            $"{ElementStrength}," +
            $"{AilmentsBitflag}," +
            $"{AilmentsChance}," +
            $"{AttackModifier}," +
            $"{DefenseModifier}," +
            $"{ActionModifier}," +
            $"{MovementModifier}," +
            $"{Special}," +
            $"{CoinCost1}," +
            $"{CoinCost2}," +
            $"{CoinCost3}," +
            $"{CoinCost4}," +
            $"{CoinCost5}," +
            $"{Multiplier}");
    }
}
