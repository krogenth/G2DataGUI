namespace G2DataGUI.Common;

/// <summary>
/// Paths for the global view of the project root: xls_data/ and map/ only —
/// these are shared between Normal and Hard, so this type has no notion of
/// game version at all. For the enemy/boss directories, which DO fork by
/// version, see VersionGamePaths — keeping them on a separate type means
/// there's no "EnemyDirectory on the wrong view" state to guard against;
/// the compiler simply won't offer that member here.
/// </summary>
public sealed class GamePaths(string root)
{
	private const string XlsDataDirectory = "xls_data";
	private const string MapDirectory = "map";

	private const string MovesFile = "MS_PARAM.BIN";
	private const string SkillsFile = "SK_PARAM.BIN";
	private const string InitialFile = "PC_INIT.BIN";
	private const string LevelupsFile = "TB_LVUP.BIN";
	private const string ManaeggsFile = "TB_MAGIC.BIN";
	private const string SkillbooksFile = "TB_SKILL.BIN";
	private const string SpecialsFile = "TB_SPCL.BIN";
	private const string ItemsFile = "ITEM.BIN";

	public string Root { get; } = root;

	private string XlsData(string fileName) =>
		Path.Combine(Root, XlsDataDirectory, fileName);

	public string Moves => XlsData(MovesFile);
	public string Skills => XlsData(SkillsFile);
	public string Initial => XlsData(InitialFile);
	public string Levelups => XlsData(LevelupsFile);
	public string Manaeggs => XlsData(ManaeggsFile);
	public string Skillbooks => XlsData(SkillbooksFile);
	public string Specials => XlsData(SpecialsFile);
	public string Items => XlsData(ItemsFile);

	public string Map =>
		Path.Combine(Root, MapDirectory);
}
