using G2DataGUI.Domain.Skills;
using G2DataGUI.Serialization.Abstractions;

namespace G2DataGUI.Serialization.Skills;

public sealed class SkillbookWriter : ICollectionDataWriter<Skillbook>
{
	public void Write(Stream stream, IEnumerable<Skillbook> items)
	{
		foreach (Skillbook skillbook in items)
		{
			foreach (SkillbookSkill skill in skillbook.Skills)
			{
				SkillbookSkillSerializer.Write(stream, skill);
			}
		}
	}

	public async Task WriteAsync(
		Stream stream,
		IEnumerable<Skillbook> items,
		CancellationToken cancellationToken = default) =>
		await Task.Run(() => Write(stream, items), cancellationToken).ConfigureAwait(false);
}
