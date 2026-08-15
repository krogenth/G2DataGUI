namespace G2DataGUI.Domain.Models;

/// <summary>
/// One bone's animated channels within an NjMotion. Kept as
/// plain keyframe lists — no interpolation/evaluation methods here, that's
/// NjPoseEvaluator's job (Rendering layer), matching NjModel's split.
/// </summary>
public sealed class NjMotionTrack
{
	public int BoneIndex { get; set; }

	public List<NjVectorKey> PositionKeys { get; set; } = [];
	public List<NjVectorKey> ScaleKeys { get; set; } = [];

	public NjRotationKind RotationKind { get; set; } = NjRotationKind.Euler32;
	public List<NjEuler32Key> EulerRotationKeys { get; set; } = [];
	public List<NjQuaternionKey> RotationKeys { get; set; } = [];
}
