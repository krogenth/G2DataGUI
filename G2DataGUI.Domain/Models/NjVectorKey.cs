using System.Numerics;

namespace G2DataGUI.Domain.Models;

/// <summary>One position or scale keyframe.</summary>
public sealed class NjVectorKey
{
	public int Frame { get; set; }
	public Vector3 Value { get; set; }
}
