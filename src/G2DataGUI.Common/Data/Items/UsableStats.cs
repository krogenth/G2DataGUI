﻿using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Items;

public class UsableStats
{
    public byte TargetEffect { get; set; }
    public byte TargetType { get; set; }
    public short Power { get; set; }
    public short Range { get; set; }
    public short CastTime { get; set; }
    public short RecoveryTime { get; set; }
    public ushort Animation { get; set; }
    public byte EffectiveOn { get; set; }
    public byte Unknown1 { get; set; }
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
    public byte BreakChance { get; set; }
    public byte Special { get; set; }
    public byte Unknown2 { get; set; }
    public byte Unknown3 { get; set; }

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

	public static uint ByteSize => 0x20;

	public UsableStats() { }

	public static UsableStats ReadUsableStats(Stream reader)
    {
		UsableStats stats = new()
		{
			TargetEffect = reader.ReadRawByte(),
			TargetType = reader.ReadRawByte(),
			Power = reader.ReadRawShort(),
			Range = reader.ReadRawShort(),
			CastTime = reader.ReadRawShort(),
			RecoveryTime = reader.ReadRawShort(),
			Animation = reader.ReadRawUShort(),
			EffectiveOn = reader.ReadRawByte(),
			Unknown1 = reader.ReadRawByte(),
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
			BreakChance = reader.ReadRawByte(),
			Special = reader.ReadRawByte(),
			Unknown2 = reader.ReadRawByte(),
			Unknown3 = reader.ReadRawByte()
		};

		return stats;
    }

    public void WriteUsableStats(Stream writer)
    {
        writer.WriteRawByte(TargetEffect);
        writer.WriteRawByte(TargetType);
        writer.WriteRawShort(Power);
        writer.WriteRawShort(Range);
        writer.WriteRawShort(CastTime);
        writer.WriteRawShort(RecoveryTime);
        writer.WriteRawUShort(Animation);
        writer.WriteRawByte(EffectiveOn);
        writer.WriteRawByte(Unknown1);
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
        writer.WriteRawByte(BreakChance);
        writer.WriteRawByte(Special);
        writer.WriteRawByte(Unknown2);
        writer.WriteRawByte(Unknown3);
    }
}
