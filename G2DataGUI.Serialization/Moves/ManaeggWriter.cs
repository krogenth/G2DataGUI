using G2DataGUI.Domain.Moves;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Moves;

public sealed class ManaeggWriter : ICollectionDataWriter<Manaegg>
{
	public void Write(Stream stream, IEnumerable<Manaegg> items)
	{
		foreach (Manaegg manaegg in items)
		{
			foreach (ManaeggSpell spell in manaegg.Spells)
			{
				ManaeggSpellSerializer.Write(stream, spell);
			}
		}
	}

	public async Task WriteAsync(
		Stream stream,
		IEnumerable<Manaegg> items,
		CancellationToken cancellationToken = default) =>
		await Task.Run(() => Write(stream, items), cancellationToken).ConfigureAwait(false);
}
