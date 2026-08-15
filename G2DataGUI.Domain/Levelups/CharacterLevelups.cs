using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Domain.Levelups;

public sealed class CharacterLevelups : IIdentifiable<int>
{
	public const int LevelsPerCharacter = 99;

	/// <summary>
	/// Position within TB_LVUP.BIN — also the index used to look
	/// up the character's display name via LocaleKeys.Characters. There is
	/// no on-disk character name; keeping the name out of the domain model
	/// is deliberate, see PORTING.md.
	/// </summary>
	public int Id { get; set; }

	public List<LevelupStats> Levels { get; set; } = [];
}
