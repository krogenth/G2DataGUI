using G2DataGUI.Domain.Skills;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Skills;

public sealed class SkillbookReader : ICollectionDataReader<Skillbook>
{
	public IReadOnlyList<Skillbook> Read(Stream stream)
	{
		List<Skillbook> result = [];

		while (stream.Position < stream.Length)
		{
			Skillbook skillbook = new() { Id = result.Count };

			for (int i = 0; i < Skillbook.SkillCount; i++)
			{
				skillbook.Skills.Add(SkillbookSkillSerializer.Read(stream));
			}

			result.Add(skillbook);
		}

		return result;
	}

	public async Task<IReadOnlyList<Skillbook>> ReadAsync(
		Stream stream,
		CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
