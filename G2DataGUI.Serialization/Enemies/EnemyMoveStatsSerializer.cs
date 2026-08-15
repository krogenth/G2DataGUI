using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Enemies;

namespace G2DataGUI.Serialization.Enemies;

public static class EnemyMoveStatsSerializer
{
	public static EnemyMoveStats Read(Stream stream) => new()
	{
		MP = stream.ReadRawShort(),
		SP = stream.ReadRawShort(),
		Unknown1 = stream.ReadRawByte(),
		TargetEffect = stream.ReadRawByte(),
		Strength = stream.ReadRawShort(),
		Power = stream.ReadRawShort(),
		AD = stream.ReadRawShort(),
		TargetType = stream.ReadRawByte(),
		NormalAttackFlag = stream.ReadRawBool(),
		Distance = stream.ReadRawShort(),
		Accuracy = stream.ReadRawShort(),
		Range = stream.ReadRawShort(),
		CastTime = stream.ReadRawShort(),
		RecoveryTime = stream.ReadRawShort(),
		Animation = stream.ReadRawByte(),
		Knockdown = stream.ReadRawBool(),
		IpStun = stream.ReadRawShort(),
		IpCancelStun = stream.ReadRawShort(),
		Knockback = stream.ReadRawShort(),
		Element = stream.ReadRawByte(),
		ElementStrength = stream.ReadRawByte(),
		AilmentsBitflag = stream.ReadRawByte(),
		AilmentsChance = stream.ReadRawByte(),
		AttackModifier = stream.ReadRawSByte(),
		DefenseModifier = stream.ReadRawSByte(),
		ActionModifier = stream.ReadRawSByte(),
		MovementModifier = stream.ReadRawSByte(),
		Special = stream.ReadRawShort()
	};

	public static void Write(Stream stream, EnemyMoveStats stats)
	{
		stream.WriteRawShort(stats.MP);
		stream.WriteRawShort(stats.SP);
		stream.WriteRawByte(stats.Unknown1);
		stream.WriteRawByte(stats.TargetEffect);
		stream.WriteRawShort(stats.Strength);
		stream.WriteRawShort(stats.Power);
		stream.WriteRawShort(stats.AD);
		stream.WriteRawByte(stats.TargetType);
		stream.WriteRawBool(stats.NormalAttackFlag);
		stream.WriteRawShort(stats.Distance);
		stream.WriteRawShort(stats.Accuracy);
		stream.WriteRawShort(stats.Range);
		stream.WriteRawShort(stats.CastTime);
		stream.WriteRawShort(stats.RecoveryTime);
		stream.WriteRawByte(stats.Animation);
		stream.WriteRawBool(stats.Knockdown);
		stream.WriteRawShort(stats.IpStun);
		stream.WriteRawShort(stats.IpCancelStun);
		stream.WriteRawShort(stats.Knockback);
		stream.WriteRawByte(stats.Element);
		stream.WriteRawByte(stats.ElementStrength);
		stream.WriteRawByte(stats.AilmentsBitflag);
		stream.WriteRawByte(stats.AilmentsChance);
		stream.WriteRawSByte(stats.AttackModifier);
		stream.WriteRawSByte(stats.DefenseModifier);
		stream.WriteRawSByte(stats.ActionModifier);
		stream.WriteRawSByte(stats.MovementModifier);
		stream.WriteRawShort(stats.Special);
	}
}
