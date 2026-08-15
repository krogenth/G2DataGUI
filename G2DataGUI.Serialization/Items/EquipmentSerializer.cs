using G2DataGUI.Domain.Items;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Items;

public static class EquipmentSerializer
{
	public static Equipment Read(Stream stream) => new()
	{
		CharacterBitflag = stream.ReadRawUShort(),
		Strength = stream.ReadRawShort(),
		Vitality = stream.ReadRawShort(),
		Action = stream.ReadRawShort(),
		Movement = stream.ReadRawShort(),
		EffectiveOn = stream.ReadRawByte(),
		FireAffinity = stream.ReadRawSByte(),
		WindAffinity = stream.ReadRawSByte(),
		EarthAffinity = stream.ReadRawSByte(),
		LightningAffinity = stream.ReadRawSByte(),
		BlizzardAffinity = stream.ReadRawSByte(),
		AilmentsBitflag = stream.ReadRawByte(),
		AilmentsChance = stream.ReadRawByte(),
		IncreaseFirePercent = stream.ReadRawSByte(),
		IncreaseWindPercent = stream.ReadRawSByte(),
		IncreaseEarthPercent = stream.ReadRawSByte(),
		IncreaseLightningPercent = stream.ReadRawSByte(),
		IncreaseBlizzardPercent = stream.ReadRawSByte(),
		IncreaseWaterPercent = stream.ReadRawSByte(),
		IncreaseExplosionPercent = stream.ReadRawSByte(),
		IncreaseForestPercent = stream.ReadRawSByte(),
		Special = stream.ReadRawShort()
	};

	public static void Write(Stream stream, Equipment equipment)
	{
		stream.WriteRawUShort(equipment.CharacterBitflag);
		stream.WriteRawShort(equipment.Strength);
		stream.WriteRawShort(equipment.Vitality);
		stream.WriteRawShort(equipment.Action);
		stream.WriteRawShort(equipment.Movement);
		stream.WriteRawByte(equipment.EffectiveOn);
		stream.WriteRawSByte(equipment.FireAffinity);
		stream.WriteRawSByte(equipment.WindAffinity);
		stream.WriteRawSByte(equipment.EarthAffinity);
		stream.WriteRawSByte(equipment.LightningAffinity);
		stream.WriteRawSByte(equipment.BlizzardAffinity);
		stream.WriteRawByte(equipment.AilmentsBitflag);
		stream.WriteRawByte(equipment.AilmentsChance);
		stream.WriteRawSByte(equipment.IncreaseFirePercent);
		stream.WriteRawSByte(equipment.IncreaseWindPercent);
		stream.WriteRawSByte(equipment.IncreaseEarthPercent);
		stream.WriteRawSByte(equipment.IncreaseLightningPercent);
		stream.WriteRawSByte(equipment.IncreaseBlizzardPercent);
		stream.WriteRawSByte(equipment.IncreaseWaterPercent);
		stream.WriteRawSByte(equipment.IncreaseExplosionPercent);
		stream.WriteRawSByte(equipment.IncreaseForestPercent);
		stream.WriteRawShort(equipment.Special);
	}
}
