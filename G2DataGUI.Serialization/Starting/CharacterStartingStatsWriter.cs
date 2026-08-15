using G2DataGUI.Domain.Starting;
using G2DataGUI.IO.Extensions;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Starting;

public sealed class CharacterStartingStatsWriter : ICollectionDataWriter<CharacterStartingStats>
{
	public void Write(Stream stream, IEnumerable<CharacterStartingStats> items)
	{
		foreach (CharacterStartingStats stats in items)
		{
			stream.WriteRawUInt(stats.Experience);
			stream.WriteRawShort(stats.Weapon);
			stream.WriteRawShort(stats.Armor);
			stream.WriteRawShort(stats.Headgear);
			stream.WriteRawShort(stats.Footwear);
			stream.WriteRawShort(stats.Accessory);
			stream.WriteRawShort(stats.Manaegg);
			stream.WriteRawShort(stats.Stamina);
			stream.WriteRawShort(stats.Unknown1);
			stream.WriteRawShort(stats.Unknown2);
			stream.WriteRawShort(stats.Unknown3);
			stream.WriteRawShort(stats.Unknown4);
			stream.WriteRawShort(stats.Unknown5);
			stream.WriteRawShort(stats.Unknown6);
			stream.WriteRawShort(stats.Unknown7);
			stream.WriteRawShort(stats.Unknown8);
			stream.WriteRawShort(stats.Unknown9);
			stream.WriteRawShort(stats.Unknown10);
			stream.WriteRawShort(stats.IpStun);
			stream.WriteRawShort(stats.IpCancelStun);
			stream.WriteRawByte(stats.ComboSpRegen);
			stream.WriteRawByte(stats.CritSpRegen);
			stream.WriteRawByte(stats.Unknown11);
			stream.WriteRawByte(stats.HitSpRegen);
			stream.WriteRawByte(stats.Unknown12);
			stream.WriteRawByte(stats.EvasionStillRate);
			stream.WriteRawByte(stats.EvasionMovingRate);
			stream.WriteRawByte(stats.KnockbackResist);
			stream.WriteRawShort(stats.Unknown13);
			stream.WriteRawShort(stats.StatusRecoveryTime);
			stream.WriteRawShort(stats.TDMG);
			stream.WriteRawShort(stats.Unknown14);
			stream.WriteRawShort(stats.THEAL);
			stream.WriteRawShort(stats.Size);
			stream.WriteRawShort(stats.Unknown15);
			stream.WriteRawShort(stats.Unknown16);
			stream.WriteRawShort(stats.Unknown17);
			stream.WriteRawShort(stats.Unknown18);
			stream.WriteRawShort(stats.Unknown19);
			stream.WriteRawShort(stats.Unknown20);
			stream.WriteRawShort(stats.Unknown21);
			stream.WriteRawShort(stats.Unknown22);
			stream.WriteRawShort(stats.Unknown23);
		}
	}

	public async Task WriteAsync(Stream stream, IEnumerable<CharacterStartingStats> items, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Write(stream, items), cancellationToken).ConfigureAwait(true);
}
