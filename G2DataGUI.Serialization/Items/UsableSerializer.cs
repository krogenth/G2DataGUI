using G2DataGUI.Domain.Items;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Items;

public static class UsableSerializer
{
	public static Usable Read(Stream stream) => new()
	{
		TargetEffect = stream.ReadRawByte(),
		TargetType = stream.ReadRawByte(),
		Power = stream.ReadRawShort(),
		Range = stream.ReadRawShort(),
		CastTime = stream.ReadRawShort(),
		RecoveryTime = stream.ReadRawShort(),
		Animation = stream.ReadRawUShort(),
		EffectiveOn = stream.ReadRawByte(),
		Unknown1 = stream.ReadRawByte(),
		IpDamage = stream.ReadRawShort(),
		IpCancelDamage = stream.ReadRawShort(),
		Knockback = stream.ReadRawShort(),
		Element = stream.ReadRawByte(),
		ElementStrength = stream.ReadRawByte(),
		AilmentsBitflag = stream.ReadRawByte(),
		AilmentsChance = stream.ReadRawByte(),
		AttackModifier = stream.ReadRawSByte(),
		DefenseModifier = stream.ReadRawSByte(),
		ActionModifier = stream.ReadRawSByte(),
		MovementModifier = stream.ReadRawSByte(),
		BreakChance = stream.ReadRawByte(),
		Special = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		Unknown3 = stream.ReadRawByte()
	};

	public static void Write(Stream stream, Usable usable)
	{
		stream.WriteRawByte(usable.TargetEffect);
		stream.WriteRawByte(usable.TargetType);
		stream.WriteRawShort(usable.Power);
		stream.WriteRawShort(usable.Range);
		stream.WriteRawShort(usable.CastTime);
		stream.WriteRawShort(usable.RecoveryTime);
		stream.WriteRawUShort(usable.Animation);
		stream.WriteRawByte(usable.EffectiveOn);
		stream.WriteRawByte(usable.Unknown1);
		stream.WriteRawShort(usable.IpDamage);
		stream.WriteRawShort(usable.IpCancelDamage);
		stream.WriteRawShort(usable.Knockback);
		stream.WriteRawByte(usable.Element);
		stream.WriteRawByte(usable.ElementStrength);
		stream.WriteRawByte(usable.AilmentsBitflag);
		stream.WriteRawByte(usable.AilmentsChance);
		stream.WriteRawSByte(usable.AttackModifier);
		stream.WriteRawSByte(usable.DefenseModifier);
		stream.WriteRawSByte(usable.ActionModifier);
		stream.WriteRawSByte(usable.MovementModifier);
		stream.WriteRawByte(usable.BreakChance);
		stream.WriteRawByte(usable.Special);
		stream.WriteRawByte(usable.Unknown2);
		stream.WriteRawByte(usable.Unknown3);
	}
}
