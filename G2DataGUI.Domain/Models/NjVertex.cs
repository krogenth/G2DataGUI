using System.Numerics;

namespace G2DataGUI.Domain.Models;

/// <summary>Bone-local — Position/Normal are relative to whichever bone(s)
/// influence this vertex, not world space. See NjBone's doc comment on why
/// world-space computation is deferred to NjPoseEvaluator instead of being
/// baked in here.</summary>
public sealed class NjVertex
{
	public int Id { get; set; }
	public Vector3 Position { get; set; }
	public Vector3 Normal { get; set; } = Vector3.UnitY;
	public List<NjInfluence> Influences { get; set; } = [];

	/// <summary>Raw per-vertex flags from the vertex chunk — meaning
	/// mostly unconfirmed beyond the NinjaFlags32 influence-slot bits
	/// already interpreted into Influences. Kept for completeness.</summary>
	public byte Flags { get; set; }
}
