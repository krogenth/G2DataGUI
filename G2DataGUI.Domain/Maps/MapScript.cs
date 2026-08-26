namespace G2DataGUI.Domain.Maps;

/// <summary>One of Map.Scripts — a map transition point: TransitionEffect
/// plus which MapID it leads to. This is the "transition between maps"
/// piece specifically, though MapID here is a raw numeric id, not yet
/// resolved to the target map's folder/file name (that resolution isn't
/// built — see MapReader's own doc comment).</summary>
public sealed class MapScript
{
	/// <summary>Position in Map.Scripts — assigned when read, not stored
	/// on disk, same "id is position in file" convention used
	/// elsewhere in this project.</summary>
	public int Index { get; set; }

	public byte[] Unknown1 { get; set; } = [];
	public System.Numerics.Vector3 Offset { get; set; }
	public float Direction { get; set; }
	public byte TransitionEffect { get; set; }
	public byte Unknown2 { get; set; }
	public byte Unknown3 { get; set; }
	public byte Unknown4 { get; set; }
	public short MapID { get; set; }
	public short Unknown5 { get; set; }
	public byte Unknown6 { get; set; }
	public byte Unknown7 { get; set; }
	public short Unknown8 { get; set; }
	public byte Unknown9 { get; set; }
	public byte Unknown10 { get; set; }
	public byte Unknown11 { get; set; }
	public byte Unknown12 { get; set; }
	public byte Unknown13 { get; set; }
	public byte Unknown14 { get; set; }
	public byte Unknown15 { get; set; }
	public byte Unknown16 { get; set; }
	public byte Unknown17 { get; set; }
	public byte[] Unknown18 { get; set; } = [];
}
