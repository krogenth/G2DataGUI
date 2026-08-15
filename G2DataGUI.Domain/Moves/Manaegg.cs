using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Domain.Moves;

public sealed class Manaegg : IIdentifiable<int>
{
	public const int SpellCount = 18;

	/// <summary>
	/// Position within TB_MAGIC.BIN — there is no on-disk id.
	/// </summary>
	public int Id { get; set; }

	public List<ManaeggSpell> Spells { get; set; } = [];
}
