using G2DataGUI.Domain.Skills;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Common;

namespace G2DataGUI.Serialization.Skills;

public sealed class SkillReader : ICollectionDataReader<Skill>
{
	public IReadOnlyList<Skill> Read(Stream stream)
	{
		List<Skill> result = [];

		while (stream.Position < stream.Length)
		{
			result.Add(new Skill
			{
				Id = result.Count,
				Name = FixedString.ReadLeftPadded(stream, Skill.MaxNameLength),
				Stats = SkillStatsSerializer.Read(stream),
				Description = FixedString.ReadRightPadded(stream, Skill.MaxDescriptionLength),
			});
		}

		return result;
	}

	public async Task<IReadOnlyList<Skill>> ReadAsync(
		Stream stream,
		CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
