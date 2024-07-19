using G2DataGUI.Common.Data.Common;
using G2DataGUI.IO.Streams;
using System.IO;

namespace G2DataGUI.Common.Data.Items;

public class EquipmentStats
{
	public ushort CharacterBitflag { get; set; }
	public short Strength { get; set; }
	public short Vitality { get; set; }
	public short Action { get; set; }
	public short Movement { get; set; }
	public byte EffectiveOn { get; set; }
	public sbyte FireAffinity { get; set; }
	public sbyte WindAffinity { get; set; }
	public sbyte EarthAffinity { get; set; }
	public sbyte LightningAffinity { get; set; }
	public sbyte BlizzardAffinity { get; set; }
	public byte AilmentsBitflag { get; set; }
	public byte AilmentsChance { get; set; }
	public sbyte IncreaseFirePercent { get; set; }
	public sbyte IncreaseWindPercent { get; set; }
	public sbyte IncreaseEarthPercent { get; set; }
	public sbyte IncreaseLightningPercent { get; set; }
	public sbyte IncreaseBlizzardPercent { get; set; }
	public sbyte IncreaseWaterPercent { get; set; }
	public sbyte IncreaseExplosionPercent { get; set; }
	public sbyte IncreaseForestPercent { get; set; }
	public short Special { get; set; }

	public bool RyudoBitflag
	{
		get => (CharacterBitflag & (ushort)Characters.CharacterTypes.Ryudo) > 0;
		set
		{
			if (value)
			{
				CharacterBitflag |= (ushort)Characters.CharacterTypes.Ryudo;
			}
			else
			{
				CharacterBitflag &= (ushort)~Characters.CharacterTypes.Ryudo;
			}
		}
	}
	public bool ElenaBitflag
	{
		get => (CharacterBitflag & (ushort)Characters.CharacterTypes.Elena) > 0;
		set
		{
			if (value)
			{
				CharacterBitflag |= (ushort)Characters.CharacterTypes.Elena;
			}
			else
			{
				CharacterBitflag &= (ushort)~Characters.CharacterTypes.Elena;
			}
		}
	}
	public bool MilleniaBitflag
	{
		get => (CharacterBitflag & (ushort)Characters.CharacterTypes.Millenia) > 0;
		set
		{
			if (value)
			{
				CharacterBitflag |= (ushort)Characters.CharacterTypes.Millenia;
			}
			else
			{
				CharacterBitflag &= (ushort)~Characters.CharacterTypes.Millenia;
			}
		}
	}
	public bool RoanBitflag
	{
		get => (CharacterBitflag & (ushort)Characters.CharacterTypes.Roan) > 0;
		set
		{
			if (value)
			{
				CharacterBitflag |= (ushort)Characters.CharacterTypes.Roan;
			}
			else
			{
				CharacterBitflag &= (ushort)~Characters.CharacterTypes.Roan;
			}
		}
	}
	public bool TioBitflag
	{
		get => (CharacterBitflag & (ushort)Characters.CharacterTypes.Tio) > 0;
		set
		{
			if (value)
			{
				CharacterBitflag |= (ushort)Characters.CharacterTypes.Tio;
			}
			else
			{
				CharacterBitflag &= (ushort)~Characters.CharacterTypes.Tio;
			}
		}
	}
	public bool MaregBitflag
	{
		get => (CharacterBitflag & (ushort)Characters.CharacterTypes.Mareg) > 0;
		set
		{
			if (value)
			{
				CharacterBitflag |= (ushort)Characters.CharacterTypes.Mareg;
			}
			else
			{
				CharacterBitflag &= (ushort)~Characters.CharacterTypes.Mareg;
			}
		}
	}
	public bool PrinceRoanBitflag
	{
		get => (CharacterBitflag & (ushort)Characters.CharacterTypes.PrinceRoan) > 0;
		set
		{
			if (value)
			{
				CharacterBitflag |= (ushort)Characters.CharacterTypes.PrinceRoan;
			}
			else
			{
				CharacterBitflag &= (ushort)~Characters.CharacterTypes.PrinceRoan;
			}
		}
	}

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

	public static uint ByteSize => 0x1C;

	public EquipmentStats() { }

	public static EquipmentStats ReadEquipmentStats(Stream reader)
	{
		EquipmentStats stats = new()
		{
			CharacterBitflag = reader.ReadRawUShort(),
			Strength = reader.ReadRawShort(),
			Vitality = reader.ReadRawShort(),
			Action = reader.ReadRawShort(),
			Movement = reader.ReadRawShort(),
			EffectiveOn = reader.ReadRawByte(),
			FireAffinity = reader.ReadRawSByte(),
			WindAffinity = reader.ReadRawSByte(),
			EarthAffinity = reader.ReadRawSByte(),
			LightningAffinity = reader.ReadRawSByte(),
			BlizzardAffinity = reader.ReadRawSByte(),
			AilmentsBitflag = reader.ReadRawByte(),
			AilmentsChance = reader.ReadRawByte(),
			IncreaseFirePercent = reader.ReadRawSByte(),
			IncreaseWindPercent = reader.ReadRawSByte(),
			IncreaseEarthPercent = reader.ReadRawSByte(),
			IncreaseLightningPercent = reader.ReadRawSByte(),
			IncreaseBlizzardPercent = reader.ReadRawSByte(),
			IncreaseWaterPercent = reader.ReadRawSByte(),
			IncreaseExplosionPercent = reader.ReadRawSByte(),
			IncreaseForestPercent = reader.ReadRawSByte(),
			Special = reader.ReadRawShort()
		};

		return stats;
	}

	public void WriteEquipmentStats(Stream writer)
	{
		writer.WriteRawUShort(CharacterBitflag);
		writer.WriteRawShort(Strength);
		writer.WriteRawShort(Vitality);
		writer.WriteRawShort(Action);
		writer.WriteRawShort(Movement);
		writer.WriteRawByte(EffectiveOn);
		writer.WriteRawSByte(FireAffinity);
		writer.WriteRawSByte(WindAffinity);
		writer.WriteRawSByte(EarthAffinity);
		writer.WriteRawSByte(LightningAffinity);
		writer.WriteRawSByte(BlizzardAffinity);
		writer.WriteRawByte(AilmentsBitflag);
		writer.WriteRawByte(AilmentsChance);
		writer.WriteRawSByte(IncreaseFirePercent);
		writer.WriteRawSByte(IncreaseWindPercent);
		writer.WriteRawSByte(IncreaseEarthPercent);
		writer.WriteRawSByte(IncreaseLightningPercent);
		writer.WriteRawSByte(IncreaseBlizzardPercent);
		writer.WriteRawSByte(IncreaseWaterPercent);
		writer.WriteRawSByte(IncreaseExplosionPercent);
		writer.WriteRawSByte(IncreaseForestPercent);
		writer.WriteRawShort(Special);
	}
}
