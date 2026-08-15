using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Enemies;
using G2DataGUI.Serialization.Common;

namespace G2DataGUI.Serialization.Enemies;

public static class EnemyStatsSerializer
{
	private const int NameLength = 18; // matches FixedLengthName.MaxLength

	public static EnemyStats Read(Stream stream) => new()
	{
		Name = FixedString.ReadLeftPadded(stream, NameLength),
		Unknown1 = stream.ReadRawByte(),
		Unknown2 = stream.ReadRawByte(),
		Type1 = stream.ReadRawByte(),
		Type2 = stream.ReadRawByte(),
		Level = stream.ReadRawShort(),
		Health = stream.ReadRawInt(),
		MP = stream.ReadRawShort(),
		SP = stream.ReadRawShort(),
		Vitality = stream.ReadRawShort(),
		Agility = stream.ReadRawShort(),
		Speed = stream.ReadRawShort(),
		Mentality = stream.ReadRawShort(),
		Stamina = stream.ReadRawShort(),
		IpStunDuration = stream.ReadRawShort(),
		IpCancelStunDuration = stream.ReadRawShort(),
		EvasionStillRate = stream.ReadRawByte(),
		EvasionMovingRate = stream.ReadRawByte(),
		FireResist = stream.ReadRawSByte(),
		WindResist = stream.ReadRawSByte(),
		EarthResist = stream.ReadRawSByte(),
		LightningResist = stream.ReadRawSByte(),
		BlizzardResist = stream.ReadRawSByte(),
		AilmentsBitflag = stream.ReadRawByte(),
		KnockbackResist = stream.ReadRawShort(),
		StatusRecoveryTime = stream.ReadRawShort(),
		TDMG = stream.ReadRawShort(),
		Unknown3 = stream.ReadRawShort(),
		THEAL = stream.ReadRawShort(),
		Size = stream.ReadRawShort(),
		Unknown4 = stream.ReadRawShort(),
		Unknown5 = stream.ReadRawByte(),
		NoRunFlag = stream.ReadRawBool(),
		Unknown6 = stream.ReadRawShort(),
		Experience = stream.ReadRawInt(),
		SkillCoins = stream.ReadRawInt(),
		MagicCoins = stream.ReadRawInt(),
		GoldCoins = stream.ReadRawInt(),
		Item1Offset = stream.ReadRawShort(),
		Item2Offset = stream.ReadRawShort(),
		Item1Chance = stream.ReadRawByte(),
		Item2Chance = stream.ReadRawByte(),
		Unknown7 = stream.ReadRawShort()
	};

	public static void Write(Stream stream, EnemyStats stats)
	{
		FixedString.WriteLeftPadded(stream, stats.Name, NameLength);
		stream.WriteRawByte(stats.Unknown1);
		stream.WriteRawByte(stats.Unknown2);
		stream.WriteRawByte(stats.Type1);
		stream.WriteRawByte(stats.Type2);
		stream.WriteRawShort(stats.Level);
		stream.WriteRawInt(stats.Health);
		stream.WriteRawShort(stats.MP);
		stream.WriteRawShort(stats.SP);
		stream.WriteRawShort(stats.Vitality);
		stream.WriteRawShort(stats.Agility);
		stream.WriteRawShort(stats.Speed);
		stream.WriteRawShort(stats.Mentality);
		stream.WriteRawShort(stats.Stamina);
		stream.WriteRawShort(stats.IpStunDuration);
		stream.WriteRawShort(stats.IpCancelStunDuration);
		stream.WriteRawByte(stats.EvasionStillRate);
		stream.WriteRawByte(stats.EvasionMovingRate);
		stream.WriteRawSByte(stats.FireResist);
		stream.WriteRawSByte(stats.WindResist);
		stream.WriteRawSByte(stats.EarthResist);
		stream.WriteRawSByte(stats.LightningResist);
		stream.WriteRawSByte(stats.BlizzardResist);
		stream.WriteRawByte(stats.AilmentsBitflag);
		stream.WriteRawShort(stats.KnockbackResist);
		stream.WriteRawShort(stats.StatusRecoveryTime);
		stream.WriteRawShort(stats.TDMG);
		stream.WriteRawShort(stats.Unknown3);
		stream.WriteRawShort(stats.THEAL);
		stream.WriteRawShort(stats.Size);
		stream.WriteRawShort(stats.Unknown4);
		stream.WriteRawByte(stats.Unknown5);
		stream.WriteRawBool(stats.NoRunFlag);
		stream.WriteRawShort(stats.Unknown6);
		stream.WriteRawInt(stats.Experience);
		stream.WriteRawInt(stats.SkillCoins);
		stream.WriteRawInt(stats.MagicCoins);
		stream.WriteRawInt(stats.GoldCoins);
		stream.WriteRawShort(stats.Item1Offset);
		stream.WriteRawShort(stats.Item2Offset);
		stream.WriteRawByte(stats.Item1Chance);
		stream.WriteRawByte(stats.Item2Chance);
		stream.WriteRawShort(stats.Unknown7);
	}
}
