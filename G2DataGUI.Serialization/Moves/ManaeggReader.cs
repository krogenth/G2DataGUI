using G2DataGUI.Domain.Moves;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Moves;

public sealed class ManaeggReader : ICollectionDataReader<Manaegg>
{
	public IReadOnlyList<Manaegg> Read(Stream stream)
	{
		List<Manaegg> result = [];

		while (stream.Position < stream.Length)
		{
			Manaegg manaegg = new() { Id = result.Count };

			for (int i = 0; i < Manaegg.SpellCount; i++)
			{
				manaegg.Spells.Add(ManaeggSpellSerializer.Read(stream));
			}

			result.Add(manaegg);
		}

		return result;
	}

	public async Task<IReadOnlyList<Manaegg>> ReadAsync(
		Stream stream,
		CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
