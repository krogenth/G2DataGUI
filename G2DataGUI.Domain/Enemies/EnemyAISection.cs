namespace G2DataGUI.Domain.Enemies;

public sealed class EnemyAISection
{
	public const int ConditionsPerSection = 5;

	public List<EnemyAI> Conditions { get; set; } = [];
}
