using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Domain.Specials;

public sealed class SpecialSet : IIdentifiable<int>
{
	public const int MovesPerSet = 6;

	/// <summary>Position within TB_SPCL.BIN — also the index used to look
	/// up the owning character's display name via LocaleKeys.SpecialChars.
	/// No on-disk character name, same convention as CharacterLevelups.Id.</summary>
	public int Id { get; set; }

	public List<SpecialMove> Moves { get; set; } = [];
}
