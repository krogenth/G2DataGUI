namespace G2DataGUI.Domain.Enemies;

public sealed class EnemyMove
{
	public string Name { get; set; } = string.Empty;
	public EnemyMoveStats Stats { get; set; } = new();
}
