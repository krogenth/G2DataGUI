using G2DataGUI.Domain.Starting;
using G2DataGUI.IO.Extensions;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Starting;

public sealed class CharacterStartingStatsReader : ICollectionDataReader<CharacterStartingStats>
{
	public IReadOnlyList<CharacterStartingStats> Read(Stream stream)
	{
		List<CharacterStartingStats> result = [];

		while (stream.Position < stream.Length)
		{
			result.Add(new CharacterStartingStats
			{
				Id = result.Count,
				Experience = stream.ReadRawUInt(),
				Weapon = stream.ReadRawShort(),
				Armor = stream.ReadRawShort(),
				Headgear = stream.ReadRawShort(),
				Footwear = stream.ReadRawShort(),
				Accessory = stream.ReadRawShort(),
				Manaegg = stream.ReadRawShort(),
				Stamina = stream.ReadRawShort(),
				Unknown1 = stream.ReadRawShort(),
				Unknown2 = stream.ReadRawShort(),
				Unknown3 = stream.ReadRawShort(),
				Unknown4 = stream.ReadRawShort(),
				Unknown5 = stream.ReadRawShort(),
				Unknown6 = stream.ReadRawShort(),
				Unknown7 = stream.ReadRawShort(),
				Unknown8 = stream.ReadRawShort(),
				Unknown9 = stream.ReadRawShort(),
				Unknown10 = stream.ReadRawShort(),
				IpStun = stream.ReadRawShort(),
				IpCancelStun = stream.ReadRawShort(),
				ComboSpRegen = stream.ReadRawByte(),
				CritSpRegen = stream.ReadRawByte(),
				Unknown11 = stream.ReadRawByte(),
				HitSpRegen = stream.ReadRawByte(),
				Unknown12 = stream.ReadRawByte(),
				EvasionStillRate = stream.ReadRawByte(),
				EvasionMovingRate = stream.ReadRawByte(),
				KnockbackResist = stream.ReadRawByte(),
				Unknown13 = stream.ReadRawShort(),
				StatusRecoveryTime = stream.ReadRawShort(),
				TDMG = stream.ReadRawShort(),
				Unknown14 = stream.ReadRawShort(),
				THEAL = stream.ReadRawShort(),
				Size = stream.ReadRawShort(),
				Unknown15 = stream.ReadRawShort(),
				Unknown16 = stream.ReadRawShort(),
				Unknown17 = stream.ReadRawShort(),
				Unknown18 = stream.ReadRawShort(),
				Unknown19 = stream.ReadRawShort(),
				Unknown20 = stream.ReadRawShort(),
				Unknown21 = stream.ReadRawShort(),
				Unknown22 = stream.ReadRawShort(),
				Unknown23 = stream.ReadRawShort(),
			});
		}

		return result;
	}

	public async Task<IReadOnlyList<CharacterStartingStats>> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
