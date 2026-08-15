using G2DataGUI.Domain.Moves;
using G2DataGUI.IO.Extensions;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Common;

namespace G2DataGUI.Serialization.Moves;

public sealed class MoveWriter : ICollectionDataWriter<Move>
{
	public void Write(Stream stream, IEnumerable<Move> items)
	{
		foreach (Move move in items)
		{
			stream.WriteRawByte((byte)move.Id);
			stream.WriteRawByte(move.Icon);
			FixedString.WriteLeftPadded(stream, move.Name, Move.MaxNameLength);
			MoveStatsSerializer.Write(stream, move.Stats);
			FixedString.WriteRightPadded(stream, move.Description, Move.MaxDescriptionLength);
		}
	}

	public async Task WriteAsync(
		Stream stream,
		IEnumerable<Move> items,
		CancellationToken cancellationToken = default) =>
		await Task.Run(() => Write(stream, items), cancellationToken).ConfigureAwait(false);
}
