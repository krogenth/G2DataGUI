using System.Numerics;

namespace G2DataGUI.Domain.Maps;

/// <summary>One of Map.Instances — an object instance placed in the map
/// (position/angle/rotation). "C" is kept as the original, not-yet-
/// understood field name rather than guessing at what it represents.</summary>
public sealed class MapInstance
{
	public ushort Id { get; set; }
	public ushort Index { get; set; }
	public short Unknown1 { get; set; }
	public byte Translation { get; set; }
	public byte Unknown2 { get; set; }
	public Vector3 Position { get; set; }
	public Vector3 Angle { get; set; }
	public Vector3 C { get; set; }
	public float Rotation { get; set; }
}
