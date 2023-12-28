using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using G2DataGUI.UI.Common.Locale;

using System.IO;

namespace G2DataGUI.Common.Data.Enemies;

public class EnemyMoveStats
{
    public short MP { get; set; }
    public short SP { get; set; }
    public byte Unknown1 { get; set; }
    public byte TargetEffect { get; set; }
    public short Strength { get; set; }
    public short Power { get; set; }
    public short AD { get; set; }
    public byte TargetType { get; set; }
    public bool NormalAttackFlag { get; set; }
    public short Distance { get; set; }
    public short Accuracy { get; set; }
    public short Range { get; set; }
    public short CastTime { get; set; }
    public short RecoveryTime { get; set; }
    public byte Animation { get; set; }
    public bool Knockdown { get; set; }
    public short IpStun { get; set; }
    public short IpCancelStun { get; set; }
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
		$"MP,SP,Unknown #1,Target Effect,Strength,Power,AD,Target Type,Normal Attack Flag,Distance," +
		$"Accuracy,Range,Cast Time,Recovery Time,Animation,Knockdown,IP Stun,IP Cancel Stun,Knockback," +
		$"Element,Element Strength,Ailments,Ailments Chance,Attack Modifier,Defense Modifier,Action Modifier," +
		$"Movement Modifier,Special";

	public static EnemyMoveStats ReadEnemyMoveStats(Stream reader)
    {
		EnemyMoveStats stats = new()
		{
			MP = reader.ReadRawShort(),
			SP = reader.ReadRawShort(),
			Unknown1 = reader.ReadRawByte(),
			TargetEffect = reader.ReadRawByte(),
			Strength = reader.ReadRawShort(),
			Power = reader.ReadRawShort(),
			AD = reader.ReadRawShort(),
			TargetType = reader.ReadRawByte(),
			NormalAttackFlag = reader.ReadRawBool(),
			Distance = reader.ReadRawShort(),
			Accuracy = reader.ReadRawShort(),
			Range = reader.ReadRawShort(),
			CastTime = reader.ReadRawShort(),
			RecoveryTime = reader.ReadRawShort(),
			Animation = reader.ReadRawByte(),
			Knockdown = reader.ReadRawBool(),
			IpStun = reader.ReadRawShort(),
			IpCancelStun = reader.ReadRawShort(),
			Knockback = reader.ReadRawShort(),
			Element = reader.ReadRawByte(),
			ElementStrength = reader.ReadRawByte(),
			AilmentsBitflag = reader.ReadRawByte(),
			AilmentsChance = reader.ReadRawByte(),
			AttackModifier = reader.ReadRawSByte(),
			DefenseModifier = reader.ReadRawSByte(),
			ActionModifier = reader.ReadRawSByte(),
			MovementModifier = reader.ReadRawSByte(),
			Special = reader.ReadRawShort()
		};

		return stats;
    }

    public void WriteEnemyMoveStats(Stream writer)
    {
        writer.WriteRawShort(MP);
        writer.WriteRawShort(SP);
        writer.WriteRawByte(Unknown1);
        writer.WriteRawByte(TargetEffect);
        writer.WriteRawShort(Strength);
        writer.WriteRawShort(Power);
        writer.WriteRawShort(AD);
        writer.WriteRawByte(TargetType);
        writer.WriteRawBool(NormalAttackFlag);
        writer.WriteRawShort(Distance);
        writer.WriteRawShort(Accuracy);
        writer.WriteRawShort(Range);
        writer.WriteRawShort(CastTime);
        writer.WriteRawShort(RecoveryTime);
        writer.WriteRawByte(Animation);
        writer.WriteRawBool(Knockdown);
        writer.WriteRawShort(IpStun);
        writer.WriteRawShort(IpCancelStun);
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
    }

	public void GenerateCSV(StreamWriter writer)
	{
		writer.Write(
			$"{MP}," +
			$"{SP}," +
			$"{Unknown1}," +
			$"{LocaleManager.Instance[LocaleKeys.TargetEffects][TargetEffect]}," +
			$"{Strength}," +
			$"{Power}," +
			$"{AD}," +
			$"{LocaleManager.Instance[LocaleKeys.TargetTypes][TargetType]}," +
			$"{NormalAttackFlag}," +
			$"{Distance}," +
			$"{Accuracy}," +
			$"{Range}," +
			$"{CastTime}," +
			$"{RecoveryTime}," +
			$"{LocaleManager.Instance[LocaleKeys.Animations][Animation]}," +
			$"{Knockdown}," +
			$"{IpStun}," +
			$"{IpCancelStun}," +
			$"{Knockback}," +
			$"{LocaleManager.Instance[LocaleKeys.Elements][Element]}," +
			$"{ElementStrength}," +
			$"{AilmentsBitflag}," +
			$"{AilmentsChance}," +
			$"{AttackModifier}," +
			$"{DefenseModifier}," +
			$"{ActionModifier}," +
			$"{MovementModifier}," +
			$"{Special}");
	}
}
