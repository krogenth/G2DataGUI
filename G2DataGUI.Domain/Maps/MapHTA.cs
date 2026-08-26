using System.Numerics;

namespace G2DataGUI.Domain.Maps;

/// <summary>One of Map.HTAs — a "hit test area": a shaped trigger volume
/// (Minimum/Maximum bounds) with a Shape/Type/Trigger/Attribute
/// classification. Most of the surrounding fields are still
/// unidentified.</summary>
public sealed class MapHTA
{
	public byte Shape { get; set; }
	public byte Type { get; set; }
	public byte Trigger { get; set; }
	public byte Unknown1 { get; set; }
	public int Unknown2 { get; set; }
	public byte Attribute { get; set; }
	public byte Unknown3 { get; set; }
	public short Unknown4 { get; set; }
	public float Unknown5 { get; set; }
	public Vector3 Minimum { get; set; }
	public Vector3 Maximum { get; set; }
	public Vector3 Unknown6 { get; set; }
	public int Unknown7 { get; set; }
	public float Unknown8 { get; set; }
	public float Unknown9 { get; set; }
	public float Unknown10 { get; set; }
	public float Unknown11 { get; set; }
}
