using System.Numerics;

namespace G2DataGUI.Domain.Models;

/// <summary>
/// One triangle — A/B/C are NjVertex.Id values (indices into NjModel's
/// flat vertex list, not raw file slot numbers; slot-to-Id resolution
/// happens once during reading — see NjModelReader). Winding is
/// A→B→C — verified against a real archive's own FBX export (every
/// sampled triangle across 5 different meshes matched Neosis's winding
/// exactly once B/C were swapped from the "natural" strip-decode order;
/// see PORTING-Chr.md).
/// </summary>
public sealed class NjTriangle
{
	public int A { get; set; }
	public int B { get; set; }
	public int C { get; set; }

	public Vector2 UvA { get; set; }
	public Vector2 UvB { get; set; }
	public Vector2 UvC { get; set; }

	public bool HasUv { get; set; }
	public int TextureIndex { get; set; } = -1;
	public byte TextureFlags { get; set; }
}
