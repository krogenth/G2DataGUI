using System.Numerics;

namespace G2DataGUI.Domain.Maps;

/// <summary>One of Map.EnemyPositions — a 2D bounding area (Minimum/
/// Maximum) plus a 3D spawn Position. Unknown2-7 are all Vector3 — kept as
/// such rather than flattened to individual floats, since three of them
/// being the exact same shape as Position is itself a hint they're
/// probably also positions/directions of some kind, even without knowing
/// which.</summary>
public sealed class MapEnemyPosition
{
	public ushort Index { get; set; }
	public short Unknown1 { get; set; }
	public Vector2 Minimum { get; set; }
	public Vector2 Maximum { get; set; }
	public Vector3 Position { get; set; }
	public Vector3 Unknown2 { get; set; }
	public Vector3 Unknown3 { get; set; }
	public Vector3 Unknown4 { get; set; }
	public Vector3 Unknown5 { get; set; }
	public Vector3 Unknown6 { get; set; }
	public Vector3 Unknown7 { get; set; }
}
