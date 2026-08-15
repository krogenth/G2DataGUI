using G2DataGUI.Domain.Specials;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Specials;

public sealed class SpecialSetWriter : ICollectionDataWriter<SpecialSet>
{
	public void Write(Stream stream, IEnumerable<SpecialSet> items)
	{
		foreach (SpecialSet set in items)
		{
			foreach (SpecialMove move in set.Moves)
			{
				SpecialMoveSerializer.Write(stream, move);
			}
		}
	}

	public async Task WriteAsync(Stream stream, IEnumerable<SpecialSet> items, CancellationToken cancellationToken = default) =>
		await Task.Run(() => Write(stream, items), cancellationToken).ConfigureAwait(true);
}
