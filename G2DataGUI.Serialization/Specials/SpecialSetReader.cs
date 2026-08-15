using G2DataGUI.Domain.Specials;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Specials;

public sealed class SpecialSetReader : ICollectionDataReader<SpecialSet>
{
	public IReadOnlyList<SpecialSet> Read(Stream stream)
	{
		List<SpecialSet> result = [];

		while (stream.Position < stream.Length)
		{
			SpecialSet set = new() { Id = result.Count };

			for (int i = 0; i < SpecialSet.MovesPerSet; i++)
			{
				set.Moves.Add(SpecialMoveSerializer.Read(stream));
			}

			result.Add(set);
		}

		return result;
	}

	public async Task<IReadOnlyList<SpecialSet>> ReadAsync(Stream stream, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
