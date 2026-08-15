using G2DataGUI.Common;
using G2DataGUI.Contexts;
using G2DataGUI.Export.Bosses;
using G2DataGUI.Export.Common;
using G2DataGUI.Export.Enemies;
using G2DataGUI.Export.Items;
using G2DataGUI.Export.Levelups;
using G2DataGUI.Export.Moves;
using G2DataGUI.Export.Skills;
using G2DataGUI.Export.Specials;
using G2DataGUI.Export.Starting;
using G2DataGUI.Locale;

namespace G2DataGUI.Services;

/// <summary>
/// Composition root for CSV export: builds each feature's exporter with
/// whatever repositories/locale it needs to resolve cross-references
/// (e.g. ManaeggSpellCsvExporter needs Global.Moves to turn a SpellOffset
/// into a name), then writes every file. Add one call here per feature as
/// it gets ported — see PORTING.md.
/// </summary>
public sealed class DataExportService(GameProjectContext project, ILocalizationService locale)
{
	public async Task ExportAllAsync(CancellationToken cancellationToken = default)
	{
		Directory.CreateDirectory(ProjectPaths.DocumentationDirectory);

		MoveCsvExporter moveExporter = new(locale, new MoveStatsCsvExporter(locale));
		await CsvFile.WriteAsync(ProjectPaths.MovesCSVPath, project.Global.Moves.Items, moveExporter, cancellationToken)
			.ConfigureAwait(false);

		ManaeggCsvExporter manaeggExporter = new(locale, new ManaeggSpellCsvExporter(project.Global.Moves));
		await CsvFile.WriteAsync(ProjectPaths.ManaeggsCSVPath, project.Global.Manaeggs.Items, manaeggExporter, cancellationToken)
			.ConfigureAwait(false);

		SkillCsvExporter skillExporter = new(new SkillStatsCsvExporter(locale));
		await CsvFile.WriteAsync(ProjectPaths.SkillsCSVPath, project.Global.Skills.Items, skillExporter, cancellationToken)
			.ConfigureAwait(false);

		SkillbookCsvExporter skillbookExporter = new(locale, new SkillbookSkillCsvExporter(project.Global.Skills));
		await CsvFile.WriteAsync(ProjectPaths.SkillbooksCSVPath, project.Global.Skillbooks.Items, skillbookExporter, cancellationToken)
			.ConfigureAwait(false);

		LevelupStatsCsvExporter levelupStatsExporter = new();
		await CsvFile.WriteAsync(
			ProjectPaths.LevelupsCSVPath,
			project.Global.Levelups.Items,
			new CharacterLevelupsCsvExporter(locale, levelupStatsExporter),
			cancellationToken
		).ConfigureAwait(false);

		await CsvFile.WriteAsync(
			ProjectPaths.LevelupsSumCSVPath,
			project.Global.Levelups.Items,
			new CharacterLevelupsSumCsvExporter(locale, levelupStatsExporter),
			cancellationToken
		).ConfigureAwait(false);

		ItemCsvExporter itemExporter = new(locale);
		await CsvFile.WriteAsync(ProjectPaths.ItemsCSVPath, project.Global.Items.Items, itemExporter, cancellationToken)
			.ConfigureAwait(false);

		SpecialSetCsvExporter specialExporter = new(locale, new SpecialMoveCsvExporter(project.Global.Moves));
		await CsvFile.WriteAsync(ProjectPaths.SpecialsCSVPath, project.Global.Specials.Items, specialExporter, cancellationToken)
			.ConfigureAwait(false);

		CharacterStartingStatsCsvExporter initialExporter = new(locale, project.Global.Items);
		await CsvFile.WriteAsync(ProjectPaths.InitialCSVPath, project.Global.Initial.Items, initialExporter, cancellationToken)
			.ConfigureAwait(false);

		await ExportCurrentVersionAsync(cancellationToken).ConfigureAwait(false);

		// TODO: Maps, once its opcode reader exists — see PORTING.md.
	}

	/// <summary>
	/// Enemies/Bosses are version-forked, so — unlike everything above —
	/// this only exports whichever version is currently active
	/// (project.CurrentVersion), to the matching *Hardmode* or plain CSV
	/// path, same as the original DifficultyMode.Instance.IsHardMode
	/// branch. To export both versions' CSVs in one pass, call
	/// project.SwitchVersionAsync for the other GameVersion and call this
	/// again — SwitchVersionAsync is cheap the second time since
	/// GameVersionContext is cached per version.
	/// </summary>
	public async Task ExportCurrentVersionAsync(CancellationToken cancellationToken = default)
	{
		bool isHardMode = project.CurrentVersion == GameVersion.Hard;

		EnemyStatsCsvExporter statsExporter = new(locale, project.Global.Items);
		EnemyAISectionCsvExporter aiExporter = new(new EnemyAICsvExporter());
		EnemyMoveCsvExporter moveExporter = new(new EnemyMoveStatsCsvExporter(locale));

		await CsvFile.WriteAsync(
			isHardMode ? ProjectPaths.EnemiesHardmodeCSVPath : ProjectPaths.EnemiesCSVPath,
			project.Current.Enemies.Items,
			new EnemyCsvExporter(statsExporter, aiExporter),
			cancellationToken
		).ConfigureAwait(false);

		await CsvFile.WriteAsync(
			isHardMode ? ProjectPaths.EnemyHardmodeMovesCSVPath : ProjectPaths.EnemyMovesCSVPath,
			project.Current.Enemies.Items,
			new EnemyMovesetCsvExporter(moveExporter),
			cancellationToken
		).ConfigureAwait(false);

		await CsvFile.WriteAsync(
			isHardMode ? ProjectPaths.BossesHardmodeCSVPath : ProjectPaths.BossesCSVPath,
			project.Current.Bosses.Items,
			new BossCsvExporter(statsExporter, aiExporter),
			cancellationToken
		).ConfigureAwait(false);

		await CsvFile.WriteAsync(
			isHardMode ? ProjectPaths.BossHardmodeMovesCSVPath : ProjectPaths.BossMovesCSVPath,
			project.Current.Bosses.Items,
			new BossMovesetCsvExporter(moveExporter),
			cancellationToken
		).ConfigureAwait(false);
	}
}
