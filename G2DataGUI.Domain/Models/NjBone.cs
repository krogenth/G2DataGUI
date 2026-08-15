using System.Numerics;

namespace G2DataGUI.Domain.Models;

/// <summary>
/// One node in the Ninja object hierarchy — every NJS_Object is a bone,
/// even ones with no mesh of their own (see NjModelReader). Flattened to a
/// list with a parent index rather than child/sibling pointers, since
/// nothing downstream (pose evaluation, rendering) needs tree traversal —
/// only "what's my world transform," which only needs the parent chain.
///
/// Position/Rotation/Scale are the BIND POSE (as read from the file) —
/// deliberately not baked into a world matrix here. World transforms are
/// computed on demand by NjPoseEvaluator, using either these bind-pose
/// values or a motion's per-frame override, so the same model data serves
/// both static viewing and animation playback. Uses System.Numerics (BCL,
/// not a project dependency) rather than reinventing vector/matrix math.
/// </summary>
public sealed class NjBone
{
	public int Index { get; set; }
	public int ParentIndex { get; set; } = -1;
	public uint EvalFlags { get; set; }

	public Vector3 Position { get; set; }

	/// <summary>
	/// Radians, already converted from the file's 32-bit BAMS
	/// angle units — see NjModelReader.
	/// </summary>
	public Vector3 Rotation { get; set; }

	public Vector3 Scale { get; set; } = Vector3.One;
}
