using G2DataGUI.Domain.Moves;
using G2DataGUI.Domain.Specials;
using G2DataGUI.Repositories.Moves;

namespace G2DataGUI.Export.Specials;

public sealed class SpecialMoveCsvExporter(MoveRepository moves)
{
	public string Header => "Special,Starting Level,Story Flag";

	public void WriteFields(TextWriter writer, SpecialMove move)
	{
		Move? resolved = moves.Find(move.MoveOffset);

		writer.Write($"{resolved?.Name ?? $"<unknown {move.MoveOffset}>"},{move.StartingLevel},{move.StoryFlag}");
	}
}
