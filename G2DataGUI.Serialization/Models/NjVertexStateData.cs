using System.Numerics;

namespace G2DataGUI.Serialization.Models;

/// <summary>
/// Local (unmapped) vertex during reading — Id is scoped to
/// whichever NjVertexStateData instance created it, NOT the final
/// Domain.Models.NjVertex.Id (see NjModelReader.MergeIntoModel for the
/// remap). Kept bone-LOCAL — no transform applied here or anywhere in
/// Serialization; see NjBone's doc comment.
/// </summary>
internal sealed class NjVertexLocal(int id, Vector3 position, Vector3 normal)
{
	public int Id { get; } = id;
	public Vector3 Position { get; set; } = position;
	public Vector3 Normal { get; set; } = normal;
	public byte Flags { get; set; }
	public List<(int BoneIndex, float Weight)> Influences { get; } = [];

	public void SetInfluence(int slot, int boneIndex, float weight)
	{
		while (Influences.Count <= slot)
		{
			Influences.Add((0, 0f));
		}

		Influences[slot] = (boneIndex, weight);
	}

	public NjVertexLocal Clone(int newId)
	{
		var clone = new NjVertexLocal(newId, Position, Normal) { Flags = Flags };
		clone.Influences.AddRange(Influences);
		return clone;
	}
}

internal sealed class NjTriangleLocal(int a, int b, int c, Vector2 uvA, Vector2 uvB, Vector2 uvC, int textureIndex, byte textureFlags, bool hasUv)
{
	public int A { get; } = a;
	public int B { get; } = b;
	public int C { get; } = c;
	public Vector2 UvA { get; } = uvA;
	public Vector2 UvB { get; } = uvB;
	public Vector2 UvC { get; } = uvC;
	public int TextureIndex { get; } = textureIndex;
	public byte TextureFlags { get; } = textureFlags;
	public bool HasUv { get; } = hasUv;
}

internal sealed class NjVertexStateData
{
	public int NextId { get; set; }
	public Dictionary<int, NjVertexLocal> CurrentSlots { get; } = [];
	public Dictionary<int, NjVertexLocal> AllVertices { get; } = [];
}

internal readonly record struct StripVertex(int Index, Vector2 Uv);
