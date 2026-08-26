using System.Numerics;

namespace G2DataGUI.Domain.Maps;

/// <summary>One of Map.Icons — a positioned map marker, optionally tied
/// to up to 3 items (Item1-3Offset — raw Item indexes, same "resolve to
/// a name later, not here" convention as EnemyStatsViewModel's
/// Item1Offset/Item2Offset).</summary>
public sealed class MapIcon
{
	public byte Id { get; set; }
	public byte Unknown1 { get; set; }
	public short Unknown2 { get; set; }
	public Vector3 Position { get; set; }
	public float Unknown3 { get; set; }
	public float YAngle { get; set; }
	public short Item1Offset { get; set; }
	public short Item2Offset { get; set; }
	public short Item3Offset { get; set; }
	public short Flag { get; set; }
}
