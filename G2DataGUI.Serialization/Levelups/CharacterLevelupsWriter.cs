using G2DataGUI.Domain.Levelups;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Levelups;

public sealed class CharacterLevelupsWriter : ICollectionDataWriter<CharacterLevelups>
{
	public void Write(Stream stream, IEnumerable<CharacterLevelups> items)
	{
		foreach (CharacterLevelups character in items)
		{
			foreach (LevelupStats level in character.Levels)
			{
				LevelupStatsSerializer.Write(stream, level);
			}
		}
	}

	public async Task WriteAsync(
		Stream stream,
		IEnumerable<CharacterLevelups> items,
		CancellationToken cancellationToken = default) =>
		await Task.Run(() => Write(stream, items), cancellationToken).ConfigureAwait(false);
}
