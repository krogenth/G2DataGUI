using System.Numerics;

namespace G2DataGUI.Domain.Models;

/// <summary>
/// One parsed NJCM model: bones (bind pose), vertices (bone-local,
/// weighted), and triangles. Deliberately holds no algorithms — no
/// skinning, no bounds computation method, no draw logic. That's
/// NjPoseEvaluator's job (in G2DataGUI.Rendering), same
/// Domain-is-pure-data split as everywhere else in this project.
/// </summary>
public sealed class NjModel
{
	public List<NjBone> Bones { get; set; } = [];
	public List<NjVertex> Vertices { get; set; } = [];
	public List<NjTriangle> Triangles { get; set; } = [];

	/// <summary>
	/// Bind-pose bounding sphere, for camera framing. Computed
	/// once at read time from bind-pose world positions — see
	/// NjModelReader. Not recomputed per animation frame.
	/// </summary>
	public Vector3 Center { get; set; }
	public float Radius { get; set; } = 1f;
}
