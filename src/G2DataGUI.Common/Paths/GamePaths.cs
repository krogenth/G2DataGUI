namespace G2DataGUI.Common.Paths;

public class GamePaths
{
    private const string XLS_DATA = "xls_data";
    public const string MovesFile = "MS_PARAM.BIN";
    public const string SkillsFile = "SK_PARAM.BIN";
    public const string InitialFile = "PC_INIT.BIN";
    public const string LevelupsFile = "TB_LVUP.BIN";
    public const string ManaeggsFile = "TB_MAGIC.BIN";
    public const string SkillbooksFile = "TB_SKILL.BIN";
    public const string SpecialsFile = "TB_SPCL.BIN";
    public const string ItemsFile = "ITEM.BIN";

    public const string MovesPath = $"{XLS_DATA}\\{MovesFile}";
    public const string SkillsPath = $"{XLS_DATA}\\{SkillsFile}";
    public const string InitialPath = $"{XLS_DATA}\\{InitialFile}";
    public const string LevelupsPath = $"{XLS_DATA}\\{LevelupsFile}";
    public const string ManaeggsPath = $"{XLS_DATA}\\{ManaeggsFile}";
    public const string SkillbooksPath = $"{XLS_DATA}\\{SkillbooksFile}";
    public const string SpecialsPath = $"{XLS_DATA}\\{SpecialsFile}";
    public const string ItemsPath = $"{XLS_DATA}\\{ItemsFile}";

    public const string EnemyDirectory = "enemy";
	public const string EnemyHardmodeDirectory = "enemy_hardmode";
	public const string BossDirectory = "boss";
	public const string BossHardmodeDirectory = "boss_hardmode";
}
