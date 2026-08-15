using G2DataGUI.Domain.Levelups;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Levelups;

public sealed class CharacterLevelupsReader : ICollectionDataReader<CharacterLevelups>
{
	public IReadOnlyList<CharacterLevelups> Read(Stream stream)
	{
		List<CharacterLevelups> result = [];

		while (stream.Position < stream.Length)
		{
			CharacterLevelups character = new() { Id = result.Count };

			for (int level = 1; level <= CharacterLevelups.LevelsPerCharacter; level++)
			{
				character.Levels.Add(LevelupStatsSerializer.Read(stream, level));
			}

			result.Add(character);
		}

		return result;
	}

	public async Task<IReadOnlyList<CharacterLevelups>> ReadAsync(
		Stream stream,
		CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(false);
}
