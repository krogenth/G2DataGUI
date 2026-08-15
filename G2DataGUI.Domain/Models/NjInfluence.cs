namespace G2DataGUI.Domain.Models;

/// <summary>One bone's contribution to a vertex's final position — a
/// vertex can have multiple influences (soft-skinned/weighted vertices,
/// NinjaFlags32 format) or exactly one (every other vertex format,
/// implicitly weighted 1.0 to whichever bone defines it).</summary>
public sealed class NjInfluence
{
	public int BoneIndex { get; set; }
	public float Weight { get; set; }
}
