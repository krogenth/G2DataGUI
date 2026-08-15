using G2DataGUI.IO.Extensions;
using G2DataGUI.Domain.Enemies;

namespace G2DataGUI.Serialization.Enemies;

/// <summary>
/// Shared by Enemy and Boss files — both are "stats + AI section + one
/// moveset, reached by seeking to a pointer stored at a fixed offset"
/// binary layout, and the pointer offsets themselves are identical for
/// both. The *_0.dat file may hold a second entry at the "second" offsets;
/// HasSecondEntry checks for it without disturbing the stream position.
///
/// This intentionally does NOT implement IDataReader/IDataWriter: those
/// read/write exactly one T with no extra parameters, but this needs an
/// isSecond flag to pick which pointer to seek to, so it stays a plain
/// static helper that Enemy/Boss readers call into.
/// </summary>
public static class EnemyEntrySerializer
{
	public const int FirstStatsPointerOffset = 0x34;
	public const int SecondStatsPointerOffset = 0x44;
	public const int FirstMovesPointerOffset = 0x3C;
	public const int SecondMovesPointerOffset = 0x4C;

	public static bool HasSecondEntry(Stream stream)
	{
		long position = stream.Position;

		stream.Seek(SecondStatsPointerOffset, SeekOrigin.Begin);
		bool hasSecond = stream.ReadRawInt() > 0;

		stream.Seek(position, SeekOrigin.Begin);

		return hasSecond;
	}

	public static (EnemyStats Stats, EnemyAISection AISection, EnemyMoveset Moveset) Read(
		Stream stream,
		bool isSecond)
	{
		stream.Seek(isSecond ? SecondStatsPointerOffset : FirstStatsPointerOffset, SeekOrigin.Begin);
		stream.Seek(stream.ReadRawInt(), SeekOrigin.Begin);

		EnemyStats stats = EnemyStatsSerializer.Read(stream);
		EnemyAISection aiSection = EnemyAISectionSerializer.Read(stream);

		stream.Seek(isSecond ? SecondMovesPointerOffset : FirstMovesPointerOffset, SeekOrigin.Begin);
		stream.Seek(stream.ReadRawInt(), SeekOrigin.Begin);

		EnemyMoveset moveset = EnemyMovesetSerializer.Read(stream);

		return (stats, aiSection, moveset);
	}

	/// <summary>
	/// Writes back into an already-existing file at its stored pointer
	/// offsets — this does NOT create the file or grow it, matching the
	/// original WriteEnemy/WriteBoss ("we need the file to write to").
	/// filePath must already exist and already contain valid pointers.
	/// </summary>
	public static void Write(
		string filePath,
		bool isSecond,
		EnemyStats stats,
		EnemyAISection aiSection,
		EnemyMoveset moveset)
	{
		if (string.IsNullOrEmpty(filePath))
		{
			return;
		}

		using FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.ReadWrite);

		stream.Seek(isSecond ? SecondStatsPointerOffset : FirstStatsPointerOffset, SeekOrigin.Begin);
		stream.Seek(stream.ReadRawInt(), SeekOrigin.Begin);

		EnemyStatsSerializer.Write(stream, stats);
		EnemyAISectionSerializer.Write(stream, aiSection);

		stream.Seek(isSecond ? SecondMovesPointerOffset : FirstMovesPointerOffset, SeekOrigin.Begin);
		stream.Seek(stream.ReadRawInt(), SeekOrigin.Begin);

		EnemyMovesetSerializer.Write(stream, moveset);
	}
}
