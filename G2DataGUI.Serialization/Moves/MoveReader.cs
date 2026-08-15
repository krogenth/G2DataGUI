using G2DataGUI.Domain.Moves;
using G2DataGUI.IO.Extensions;
using G2DataGUI.Serialization.Abstractions;
using G2DataGUI.Serialization.Common;

namespace G2DataGUI.Serialization.Moves;

public sealed class MoveReader : ICollectionDataReader<Move>
{
	public IReadOnlyList<Move> Read(Stream stream)
	{
		List<Move> result = [];

		while (stream.Position < stream.Length)
		{
			Move move = new()
			{
				Id = result.Count,
			};

			// On-disk byte 0 of each entry is unused by the game; MoveWriter
			// overwrites it with Id on save purely for hex-editor readability.
			// We still have to consume the byte here to stay aligned.
			_ = stream.ReadRawByte();

			move.Icon = stream.ReadRawByte();
			move.Name = FixedString.ReadLeftPadded(stream, Move.MaxNameLength);
			move.Stats = MoveStatsSerializer.Read(stream);
			move.Description = FixedString.ReadRightPadded(stream, Move.MaxDescriptionLength);

			result.Add(move);
		}

		return result;
	}

	public async Task<IReadOnlyList<Move>> ReadAsync(
		Stream stream,
		CancellationToken cancellationToken = default) =>
		await Task.Run(() => Read(stream), cancellationToken).ConfigureAwait(true);
}
