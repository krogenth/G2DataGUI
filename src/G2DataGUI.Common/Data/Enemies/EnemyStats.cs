using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyStats
{
    public byte Unknown1 { get; set; }
    public byte Unknown2 { get; set; }
    public byte Type1 { get; set; }
    public byte Type2 { get; set; }
    public short Level { get; set; }
    public int Health { get; set; }
    public short MP { get; set; }
    public short SP { get; set; }
    public short Vitality { get; set; }
    public short Agility { get; set; }
    public short Speed { get; set; }
    public short Mentality { get; set; }
    public short Stamina { get; set; }
    public short IpStunDuration { get; set; }
    public short IpCancelStunDuration { get; set; }
    public byte EvasionStillRate { get; set; }
    public byte EvasionMovingRate { get; set; }
    public sbyte FireResist { get; set; }
    public sbyte WindResist { get; set; }
    public sbyte EarthResist { get; set; }
    public sbyte LightningResist { get; set; }
    public sbyte BlizzardResist { get; set; }
    public byte AilmentsBitflag { get; set; }
    public short KnockbackResist { get; set; }
    public short StatusRecoveryTime { get; set; }
    public short T_DMG { get; set; }
    public short Unknown3 { get; set; }
    public short T_HEAL { get; set; }
    public short Size { get; set; }
    public short Unknown4 { get; set; }
    public byte Unknown5 { get; set; }
    public bool NoRunFlag { get; set; }
    public short Unknown6 { get; set; }
    public int Experience { get; set; }
    public int SkillCoins { get; set; }
    public int MagicCoins { get; set; }
    public int GoldCoins { get; set; }
    public short Item1Offset { get; set; }
    public short Item2Offset { get; set; }
    public byte Item1Chance { get; set; }
    public byte Item2Chance { get; set; }
    public short Unknown7 { get; set; }

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

    public static EnemyStats ReadEnemyStats(Stream reader)
    {
        EnemyStats stats = new EnemyStats();
        stats.Unknown1 = reader.ReadRawByte();
        stats.Unknown2 = reader.ReadRawByte();
        stats.Type1 = reader.ReadRawByte();
        stats.Type2 = reader.ReadRawByte();
        stats.Level = reader.ReadRawShort();
        stats.Health = reader.ReadRawInt();
        stats.MP = reader.ReadRawShort();
        stats.SP = reader.ReadRawShort();
        stats.Vitality = reader.ReadRawShort();
        stats.Agility = reader.ReadRawShort();
        stats.Speed = reader.ReadRawShort();
        stats.Mentality = reader.ReadRawShort();
        stats.Stamina = reader.ReadRawShort();
        stats.IpStunDuration = reader.ReadRawShort();
        stats.IpCancelStunDuration = reader.ReadRawShort();
        stats.EvasionStillRate = reader.ReadRawByte();
        stats.EvasionMovingRate = reader.ReadRawByte();
        stats.FireResist = reader.ReadRawSByte();
        stats.WindResist = reader.ReadRawSByte();
        stats.EarthResist = reader.ReadRawSByte();
        stats.LightningResist = reader.ReadRawSByte();
        stats.BlizzardResist = reader.ReadRawSByte();
        stats.AilmentsBitflag = reader.ReadRawByte();
        stats.KnockbackResist = reader.ReadRawShort();
        stats.StatusRecoveryTime = reader.ReadRawShort();
        stats.T_DMG = reader.ReadRawShort();
        stats.Unknown3 = reader.ReadRawShort();
        stats.T_HEAL = reader.ReadRawShort();
        stats.Size = reader.ReadRawShort();
        stats.Unknown4 = reader.ReadRawShort();
        stats.Unknown5 = reader.ReadRawByte();
        stats.NoRunFlag = reader.ReadRawBool();
        stats.Unknown6 = reader.ReadRawShort();
        stats.Experience = reader.ReadRawInt();
        stats.SkillCoins = reader.ReadRawInt();
        stats.MagicCoins = reader.ReadRawInt();
        stats.GoldCoins = reader.ReadRawInt();
        stats.Item1Offset = reader.ReadRawShort();
        stats.Item2Offset = reader.ReadRawShort();
        stats.Item1Chance = reader.ReadRawByte();
        stats.Item2Chance = reader.ReadRawByte();
        stats.Unknown7 = reader.ReadRawShort();

        return stats;
    }

    public void WriteEnemyStats(Stream writer)
    {
        writer.WriteRawByte(Unknown1);
        writer.WriteRawByte(Unknown2);
        writer.WriteRawByte(Type1);
        writer.WriteRawByte(Type2);
        writer.WriteRawShort(Level);
        writer.WriteRawInt(Health);
        writer.WriteRawShort(MP);
        writer.WriteRawShort(SP);
        writer.WriteRawShort(Vitality);
        writer.WriteRawShort(Agility);
        writer.WriteRawShort(Speed);
        writer.WriteRawShort(Mentality);
        writer.WriteRawShort(Stamina);
        writer.WriteRawShort(IpStunDuration);
        writer.WriteRawShort(IpCancelStunDuration);
        writer.WriteRawByte(EvasionStillRate);
        writer.WriteRawByte(EvasionMovingRate);
        writer.WriteRawSByte(FireResist);
        writer.WriteRawSByte(WindResist);
        writer.WriteRawSByte(EarthResist);
        writer.WriteRawSByte(LightningResist);
        writer.WriteRawSByte(BlizzardResist);
        writer.WriteRawByte(AilmentsBitflag);
        writer.WriteRawShort(KnockbackResist);
        writer.WriteRawShort(StatusRecoveryTime);
        writer.WriteRawShort(T_DMG);
        writer.WriteRawShort(Unknown3);
        writer.WriteRawShort(T_HEAL);
        writer.WriteRawShort(Size);
        writer.WriteRawShort(Unknown4);
        writer.WriteRawByte(Unknown5);
        writer.WriteRawBool(NoRunFlag);
        writer.WriteRawShort(Unknown6);
        writer.WriteRawInt(Experience);
        writer.WriteRawInt(SkillCoins);
        writer.WriteRawInt(MagicCoins);
        writer.WriteRawInt(GoldCoins);
        writer.WriteRawShort(Item1Offset);
        writer.WriteRawShort(Item2Offset);
        writer.WriteRawByte(Item1Chance);
        writer.WriteRawByte(Item2Chance);
        writer.WriteRawShort(Unknown7);
    }
}