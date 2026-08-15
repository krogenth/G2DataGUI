using G2DataGUI.Domain.Abstractions;

namespace G2DataGUI.Domain.Moves;

/// <summary>
/// One entry from MS_PARAM.BIN. Pure data — no file I/O, no CSV, no locale
/// lookups. See G2DataGUI.Serialization.Moves.MoveReader/MoveWriter for the
/// binary layout, and G2DataGUI.Export.Moves.MoveCsvExporter for CSV.
/// </summary>
public sealed class Move : IIdentifiable<int>
{
	/// <summary>
	/// Position of this entry within MS_PARAM.BIN. NOT read from the file —
	/// the on-disk id byte is unused by the game and is overwritten with
	/// this value on write purely so the file is easier to read in a hex
	/// editor. See MoveReader/MoveWriter.
	/// </summary>
	public int Id { get; set; }

	public byte Icon { get; set; }
	public string Name { get; set; } = string.Empty;
	public MoveStats Stats { get; set; } = new();
	public string Description { get; set; } = string.Empty;

	public const int MaxNameLength = 18;
	public const int MaxDescriptionLength = 40;
}
