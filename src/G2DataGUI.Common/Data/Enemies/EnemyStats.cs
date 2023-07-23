using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using G2DataGUI.UI.Common.Locale;

using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyStats : BaseContainer
{
	private FixedLengthName _name;
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
    public short TDMG { get; set; }
    public short Unknown3 { get; set; }
    public short THEAL { get; set; }
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

	public string Name
	{
		get => _name.Name;
		set
		{
			_name.Name = value;
			NotifyPropertyChanged(nameof(Name));
		}
	}
	public uint MaxNameLength { get => FixedLengthName.MaxLength; }

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
		$"Name,Unknown #1,Unknown #2,Type #1,Type #2,Level,Health,MP,SP,Vitality,Agility,Speed," +
		$"Mentality,Stamina,IP Stun Duration,IP Cancel Stun Duration,Still Evasion Rate,Moving Evasion Rate," +
		$"Fire Resist,Wind Resist,Earth Resist,Lightning Resist,Blizzard Resist,Ailments," +
		$"Knockback Resist,Status Recovery Time,TDMG,Unknown #3,THEAL,Size,Unknown #4,Unknown #5,No Run Flag," +
		$"Unknown #6,Experience,Skill Coins,Magic Coins,Gold Coins,Item #1,Item #2,Item #1 Chance,Item #2 Chance," +
		$"Unknown #7";

    public static EnemyStats ReadEnemyStats(Stream reader)
    {
		EnemyStats stats = new()
		{
			_name = FixedLengthName.ReadFixedLengthName(reader),
			Unknown1 = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Type1 = reader.ReadRawByte(),
			Type2 = reader.ReadRawByte(),
			Level = reader.ReadRawShort(),
			Health = reader.ReadRawInt(),
			MP = reader.ReadRawShort(),
			SP = reader.ReadRawShort(),
			Vitality = reader.ReadRawShort(),
			Agility = reader.ReadRawShort(),
			Speed = reader.ReadRawShort(),
			Mentality = reader.ReadRawShort(),
			Stamina = reader.ReadRawShort(),
			IpStunDuration = reader.ReadRawShort(),
			IpCancelStunDuration = reader.ReadRawShort(),
			EvasionStillRate = reader.ReadRawByte(),
			EvasionMovingRate = reader.ReadRawByte(),
			FireResist = reader.ReadRawSByte(),
			WindResist = reader.ReadRawSByte(),
			EarthResist = reader.ReadRawSByte(),
			LightningResist = reader.ReadRawSByte(),
			BlizzardResist = reader.ReadRawSByte(),
			AilmentsBitflag = reader.ReadRawByte(),
			KnockbackResist = reader.ReadRawShort(),
			StatusRecoveryTime = reader.ReadRawShort(),
			TDMG = reader.ReadRawShort(),
			Unknown3 = reader.ReadRawShort(),
			THEAL = reader.ReadRawShort(),
			Size = reader.ReadRawShort(),
			Unknown4 = reader.ReadRawShort(),
			Unknown5 = reader.ReadRawByte(),
			NoRunFlag = reader.ReadRawBool(),
			Unknown6 = reader.ReadRawShort(),
			Experience = reader.ReadRawInt(),
			SkillCoins = reader.ReadRawInt(),
			MagicCoins = reader.ReadRawInt(),
			GoldCoins = reader.ReadRawInt(),
			Item1Offset = reader.ReadRawShort(),
			Item2Offset = reader.ReadRawShort(),
			Item1Chance = reader.ReadRawByte(),
			Item2Chance = reader.ReadRawByte(),
			Unknown7 = reader.ReadRawShort()
		};

		return stats;
    }

    public void WriteEnemyStats(Stream writer)
    {
		_name.WriteFixedLengthName(writer);
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
        writer.WriteRawShort(TDMG);
        writer.WriteRawShort(Unknown3);
        writer.WriteRawShort(THEAL);
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

	public void GenerateCSV(StreamWriter writer)
	{
		writer.Write(
			$"{Name}," +
			$"{Unknown1}," +
			$"{Unknown2}," +
			$"{LocaleManager.Instance[LocaleKeys.EnemyTypes][Type1]}," +
			$"{LocaleManager.Instance[LocaleKeys.EnemyTypes][Type2]}," +
			$"{Level}," +
			$"{Health}," +
			$"{MP}," +
			$"{SP}," +
			$"{Vitality}," +
			$"{Agility}," +
			$"{Speed}," +
			$"{Mentality}," +
			$"{Stamina}," +
			$"{IpStunDuration}," +
			$"{IpCancelStunDuration}," +
			$"{EvasionStillRate}," +
			$"{EvasionMovingRate}," +
			$"{FireResist}," +
			$"{WindResist}," +
			$"{EarthResist}," +
			$"{LightningResist}," +
			$"{BlizzardResist}," +
			$"{AilmentsBitflag}," +
			$"{KnockbackResist}," +
			$"{StatusRecoveryTime}," +
			$"{TDMG}," +
			$"{Unknown3}," +
			$"{THEAL}," +
			$"{Size}," +
			$"{Unknown4}," +
			$"{Unknown5}," +
			$"{NoRunFlag}," +
			$"{Unknown6}," +
			$"{Experience}," +
			$"{SkillCoins}," +
			$"{MagicCoins}," +
			$"{GoldCoins}," +
			$"{Items.Items.Instance.GameItems[Item1Offset].Name}," +
			$"{Items.Items.Instance.GameItems[Item2Offset].Name}," +
			$"{Item1Chance}," +
			$"{Item2Chance}," +
			$"{Unknown7}");
	}
}
