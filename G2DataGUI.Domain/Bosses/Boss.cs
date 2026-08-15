using G2DataGUI.Domain.Abstractions;
using G2DataGUI.Domain.Enemies;

namespace G2DataGUI.Domain.Bosses;

/// <summary>
/// Binary-identical to Enemy (same EnemyStats/EnemyAISection layout, same
/// pointer offsets) — kept as a separate type because bosses live in a
/// separate directory/repository and get separate CSV output, matching the
/// original project's split. Movesets is a list because the original
/// intended an additional zXX_act.dat to contribute extra movesets (see the
/// doc comment on the old Boss.cs) — but the old ReadBoss/WriteBoss never
/// actually read that file, only ever populating index 0. Ported as-is:
/// still a single-entry list, not a real gap introduced by this port.
/// </summary>
public sealed class Boss : IIdentifiable<int>
{
	public int Id { get; set; }

	public EnemyStats Stats { get; set; } = new();
	public EnemyAISection AISection { get; set; } = new();
	public List<EnemyMoveset> Movesets { get; set; } = [];

	public bool IsSecond { get; set; }
	public string FilePath { get; set; } = string.Empty;
}
