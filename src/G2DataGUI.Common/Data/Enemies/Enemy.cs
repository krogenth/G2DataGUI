using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System;
using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class Enemy : BaseContainer
{
    private FixedLengthName _name;

    private EnemyStats _stats;
    private EnemyAISection _AIs;
    private EnemyMoveset _moveset;

    public bool IsSecond { get; set; }
    public string File { get; set; } = "";

    public static Enemy ReadEnemy(Stream reader, string file, bool isSecond = false)
    {
        Enemy enemy = new Enemy();
        enemy.File = file;
        enemy.IsSecond = isSecond;

        // first enemy instance offset is always 0x34
        // second enemy instance offset is always 0x44
        if (isSecond) reader.Seek(0x44, SeekOrigin.Begin);
        else reader.Seek(0x34, SeekOrigin.Begin);
        reader.Seek(reader.ReadRawInt(), SeekOrigin.Begin);

        FixedLengthName? name = reader.ReadStruct<FixedLengthName>();
        if (name != null) enemy._name = (FixedLengthName)name;

        enemy._AIs = EnemyAISection.ReadEnemyAISection(reader);

        // first enemy moveset instance offset is always 0x3C
        // second enemy moveset instance offset is always 0x4C
        if (isSecond) reader.Seek(0x4C, SeekOrigin.Begin);
        else reader.Seek(0x3C, SeekOrigin.Begin);
        reader.Seek(reader.ReadRawInt(), SeekOrigin.Begin);

        enemy._moveset = EnemyMoveset.ReadEnemyMoveset(reader);

        return enemy;
    }

    public static bool FileHasSecondEnemy(Stream reader)
    {
        // second enemy instance offset is always at 0x44
        if (reader == null) return false;
        long position = reader.Position;
        reader.Seek(0x44, SeekOrigin.Begin);
        bool hasSecond = reader.ReadRawInt() > 0;
        reader.Seek(position, SeekOrigin.Begin);
        return hasSecond;
    }

    public string Name { get => _name.Name; set { _name.Name = value; NotifyPropertyChanged(nameof(Name)); } }
    public byte Unknown1 { get => _stats.Unknown1; set => _stats.Unknown1 = value; }
    public byte Unknown2 { get => _stats.Unknown2; set => _stats.Unknown2 = value; }
    public byte Type1 { get => _stats.Type1; set => _stats.Type1 = value; }
    public byte Type2 { get => _stats.Type2; set => _stats.Type2 = value; }
    public short Level { get => _stats.Level; set => _stats.Level = value; }
    public int Health { get => _stats.Health; set => _stats.Health = value; }
    public short Mp { get => _stats.Mp; set => _stats.Mp = value; }
    public short Sp { get => _stats.Sp; set => _stats.Sp = value; }
    public short Vitality { get => _stats.Vitality; set => _stats.Vitality = value; }
    public short Agility { get => _stats.Agility; set => _stats.Agility = value; }
    public short Speed { get => _stats.Speed; set => _stats.Speed = value; }
    public short Mentality { get => _stats.Mentality; set => _stats.Mentality = value; }
    public short Stamina { get => _stats.Stamina; set => _stats.Stamina = value; }
    public short IpStunResist { get => _stats.IpStunResist; set => _stats.IpStunResist = value; }
    public short IpCancelStunResist { get => _stats.IpCancelStunResist; set => _stats.IpCancelStunResist = value; }
    public byte EvasionStillRate { get => _stats.EvasionStillRate; set => _stats.EvasionStillRate = value; }
    public byte EvasionMovingRate { get => _stats.EvasionMovingRate; set => _stats.EvasionMovingRate = value; }
    public byte FireResist { get => _stats.FireResist; set => _stats.FireResist = value; }
    public byte WindResist { get => _stats.WindResist; set => _stats.WindResist = value; }
    public byte EarthResist { get => _stats.EarthResist; set => _stats.EarthResist = value; }
    public byte LightningResist { get => _stats.LightningResist; set => _stats.LightningResist = value; }
    public byte BlizzardResist { get => _stats.BlizzardResist; set => _stats.BlizzardResist = value; }
    public bool PoisonBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Poison) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Poison;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Poison;
        }
    }
    public bool SleepBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Sleep) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Sleep;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Sleep;
        }
    }
    public bool ParalysisBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Paralysis) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Paralysis;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Paralysis;
        }
    }
    public bool ConfusionBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Confusion) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Confusion;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Confusion;
        }
    }
    public bool PlagueBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Plague) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Plague;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Plague;
        }
    }
    public bool Magic_BlockBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Magic_Block) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Magic_Block;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Magic_Block;
        }
    }
    public bool Move_BlockBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Move_Block) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Move_Block;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Move_Block;
        }
    }
    public bool DeathBitflag
    {
        get => (_stats.AilmentsBitflag & (byte)Ailments.AilmentTypes.Death) > 0;
        set
        {
            if (value) _stats.AilmentsBitflag |= (byte)Ailments.AilmentTypes.Death;
            else _stats.AilmentsBitflag &= (byte)~Ailments.AilmentTypes.Death;
        }
    }
    public short KnockbackResist { get =>  _stats.KnockbackResist; set => _stats.KnockbackResist = value; }
    public short T_REC { get => _stats.T_REC; set => _stats.T_REC = value; }
    public short T_DMG { get => _stats.T_DMG; set => _stats.T_DMG = value; }
    public short Unknown3 { get => _stats.Unknown3; set => _stats.Unknown3 = value; }
    public short T_HEAL { get => _stats.T_HEAL; set => _stats.T_HEAL = value; }
    public short Size { get => _stats.Size; set => _stats.Size = value; }
    public short Unknown4 { get => _stats.Unknown4; set => _stats.Unknown4 = value; }
    public byte Unknown5 { get => _stats.Unknown5; set => _stats.Unknown5 = value; }
    public bool NoRunFlag { get => Convert.ToBoolean(_stats.NoRunFlag); set => _stats.NoRunFlag = Convert.ToByte(value); }
    public short Unknown6 { get => _stats.Unknown6; set => _stats.Unknown6 = value; }
    public int Experience { get => _stats.Experience; set => _stats.Experience = value; }
    public int SkillCoins { get => _stats.SkillCoins; set => _stats.SkillCoins = value; }
    public int MagicCoins { get => _stats.MagicCoins; set => _stats.MagicCoins = value; }
    public int GoldCoins { get => _stats.GoldCoins; set => _stats.GoldCoins = value; }
    public short Item1Offset { get => _stats.Item1Offset; set => _stats.Item1Offset = value; }
    public short Item2Offset { get => _stats.Item2Offset; set => _stats.Item2Offset = value; }
    public byte Item1Chance { get => _stats.Item1Chance; set => _stats.Item1Chance = value; }
    public byte Item2Chance { get => _stats.Item2Chance; set => _stats.Item2Chance = value; }
    public short Unknown7 { get => _stats.Unknown7; set => _stats.Unknown7 = value; }
}