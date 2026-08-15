using G2DataGUI.Domain.Skills;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Common;

namespace G2DataGUI.Serialization.Skills;

public sealed class SkillWriter : ICollectionDataWriter<Skill>
{
	public void Write(Stream stream, IEnumerable<Skill> items)
	{
		foreach (Skill skill in items)
		{
			FixedString.WriteLeftPadded(stream, skill.Name, Skill.MaxNameLength);
			SkillStatsSerializer.Write(stream, skill.Stats);
			FixedString.WriteRightPadded(stream, skill.Description, Skill.MaxDescriptionLength);
		}
	}

	public async Task WriteAsync(
		Stream stream,
		IEnumerable<Skill> items,
		CancellationToken cancellationToken = default) =>
		await Task.Run(() => Write(stream, items), cancellationToken).ConfigureAwait(true);
}
