using G2DataGUI.Domain.Moves;
using G2DataGUI.IO.Extensions;

namespace G2DataGUI.Serialization.Moves;

public static class MoveStatsSerializer
{
	public static MoveStats Read(Stream stream) => new()
	{
		Cost = stream.ReadRawShort(),
		TargetEffect = stream.ReadRawByte(),
		TargetType = stream.ReadRawByte(),
		Strength = stream.ReadRawShort(),
		Power = stream.ReadRawShort(),
		Range = stream.ReadRawShort(),
		Cast1Time = stream.ReadRawShort(),
		Cast5Time = stream.ReadRawShort(),
		RecoveryTime = stream.ReadRawShort(),
		Animation = stream.ReadRawShort(),
		Unknown1 = stream.ReadRawByte(),
		Knockdown = stream.ReadRawBool(),
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
		Special = stream.ReadRawShort(),
		CoinCost1 = stream.ReadRawShort(),
		CoinCost2 = stream.ReadRawShort(),
		CoinCost3 = stream.ReadRawShort(),
		CoinCost4 = stream.ReadRawShort(),
		CoinCost5 = stream.ReadRawShort(),
		Multiplier = stream.ReadRawShort(),
	};

	public static void Write(Stream stream, MoveStats stats)
	{
		stream.WriteRawShort(stats.Cost);
		stream.WriteRawByte(stats.TargetEffect);
		stream.WriteRawByte(stats.TargetType);
		stream.WriteRawShort(stats.Strength);
		stream.WriteRawShort(stats.Power);
		stream.WriteRawShort(stats.Range);
		stream.WriteRawShort(stats.Cast1Time);
		stream.WriteRawShort(stats.Cast5Time);
		stream.WriteRawShort(stats.RecoveryTime);
		stream.WriteRawShort(stats.Animation);
		stream.WriteRawByte(stats.Unknown1);
		stream.WriteRawBool(stats.Knockdown);
		stream.WriteRawShort(stats.IpDamage);
		stream.WriteRawShort(stats.IpCancelDamage);
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
		stream.WriteRawShort(stats.CoinCost1);
		stream.WriteRawShort(stats.CoinCost2);
		stream.WriteRawShort(stats.CoinCost3);
		stream.WriteRawShort(stats.CoinCost4);
		stream.WriteRawShort(stats.CoinCost5);
		stream.WriteRawShort(stats.Multiplier);
	}
}
