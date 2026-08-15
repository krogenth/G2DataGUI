namespace G2DataGUI.Domain.Enemies;

public sealed class EnemyMoveset
{
	public const int MovesPerSet = 5;

	public List<EnemyMove> Moves { get; set; } = [];
}
