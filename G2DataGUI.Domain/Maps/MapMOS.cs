using System.Numerics;

namespace G2DataGUI.Domain.Maps;

/// <summary>One of Map.MOSs — positioned, not yet otherwise understood
/// (the name itself is carried over unexplained from the original
/// investigation of this format).</summary>
public sealed class MapMOS
{
	public ushort Id { get; set; }
	public ushort Index { get; set; }
	public byte Unknown1 { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public Vector3 Position { get; set; }
	public short Unknown5 { get; set; }
	public short Unknown6 { get; set; }
	public short Unknown7 { get; set; }
	public short Unknown8 { get; set; }
	public short Unknown9 { get; set; }
	public short Unknown10 { get; set; }
}
