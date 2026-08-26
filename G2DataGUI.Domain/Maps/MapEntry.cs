using System.Numerics;

namespace G2DataGUI.Domain.Maps;

/// <summary>One of Map.Entries — a positioned point in the map with a
/// facing direction. Unknown2/Unknown3 are read as raw int rather than
/// uint (unlike most of this format) — kept exactly as originally
/// determined, not normalized to match the surrounding convention, since
/// that distinction may be meaningful (signed data) rather than
/// arbitrary.</summary>
public sealed class MapEntry
{
	public uint Id { get; set; }
	public Vector3 Position { get; set; }
	public float Direction { get; set; }
	public float Unknown1 { get; set; }
	public int Unknown2 { get; set; }
	public int Unknown3 { get; set; }
}
